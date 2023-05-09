using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Utilities;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;


namespace Property_Management.BLL.Implementations
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Transaction> _transRepo;
        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IRepository<Payment> _paymentRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISendMailService _sendMailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string? _token;
        private PayStackApi PaystackApi { get; set; }

        public PaymentServices(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, ISendMailService sendMailService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _sendMailService = sendMailService;
            _transRepo = _unitOfWork.GetRepository<Transaction>();
            _token = _configuration["PayMent:PayStackApiKey"];
            PaystackApi = new PayStackApi(_token);
            _paymentRepo = _unitOfWork.GetRepository<Payment>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
        }

        public async Task<IEnumerable<TransactionResponse>> GetAllPayment()
        {
            IEnumerable<Transaction> transacation = await _transRepo.GetByAsync(trans => trans.Amount > decimal.Zero);
            if (transacation == null)
                throw new InvalidOperationException($"Empty transaction list.");

            return transacation.Select(l => new TransactionResponse
            {
                Id = l.Id,
                Name = l.Name,
                Email = l.Email,
                Status = l.Status ? "Paid" : "Not Verified.",
                TransactionRefereal = l.TransactionRefereal,
                MadeAt = l.MadeAt.ToLongDateString()
            });
        }


        public async Task<TransactionResponse> GetPayment(string Id)
        {
            Transaction transaction = await _transRepo.GetSingleByAsync(trans => trans.Amount > decimal.Zero);
            if (transaction == null)
                throw new InvalidOperationException($"Empty transaction list. this transaction id does not exist.");

            return new TransactionResponse
            {
                Id = transaction.Id,
                Name = transaction.Name,
                Email = transaction.Email,
                Status = transaction.Status ? "Paid" : "Not Verified.",
                TransactionRefereal = transaction.TransactionRefereal,
                MadeAt = transaction.MadeAt.ToLongDateString()
            };
        }

        public async Task<PaymentResponse> MakePayment(PaymentRequest request)
        {
            Tenant? tenant = await _tenantRepo.GetSingleByAsync(t => t.Email == request.Email);
            if (tenant == null)
                throw new InvalidOperationException($"user with the email {request.Email} was not found.");

            string refernceKey = RandomNums.GenerateRandomNumbers().ToString();
            TransactionInitializeRequest transRequest = new()
            {
                AmountInKobo = request.Amount * 100, // 1 kobo * 100 = 1 Naira
                Email = request.Email,
                Reference = refernceKey,
                Currency = "NGN",
                CallbackUrl = "https://localhost:7258/api/payment/verify-payment",
            };

            TransactionInitializeResponse response = PaystackApi.Transactions.Initialize(transRequest);
            if (!response.Status)
                throw new InvalidOperationException($"Sorry! error occured while performing the transaction. this is what happended: {response.Message} . Do try again with valid deatils.");

            Transaction newTransaction = _mapper.Map<Transaction>(request);
            newTransaction.TransactionRefereal = transRequest.Reference;
            await _transRepo.AddAsync(newTransaction);

            Payment newPayment = new Payment()
            {
                LeaseId = tenant.LeaseId,
                TenantId = tenant.TenantId,
                Amount = request.Amount,
                IsDeleted = false,
                PaymentDate = DateTime.UtcNow,
                PaymentType = request.PaymentType,
            };
            await _paymentRepo.AddAsync(newPayment);
            return new PaymentResponse
            {
                Message = $"{request.Name} your Payment link has been generated: {response.Data.AuthorizationUrl} use this link to complete your payment.",
                PaymentFor = $"{request.PaymentFor} payment",
                TransactionAmount = request.Amount,
                PaymentLink = response.Data.AuthorizationUrl,
                ReferenceKey = refernceKey,
            };
        }

        public async Task<Response> VerifyPayment(string userId, string reference)
        {
            TransactionVerifyResponse response = PaystackApi.Transactions.Verify(reference);
            if (response.Data.Status != "success")
                throw new InvalidOperationException("Sorry! verification failed. Error occured while trying to verify the transaction. It seems your payment was not successful.");

            Transaction transaction = await _transRepo.GetSingleByAsync(trans => trans.TransactionRefereal == reference);
            if (transaction == null)
                throw new InvalidOperationException($"Transaction was not found. {response.Data.GatewayResponse}"); 

            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException($"User with the id {userId} was not found.");

            transaction.Status = true;
            await _transRepo.UpdateAsync(transaction);
            string message = $"Payment verification succesful. {response.Data.GatewayResponse}";
           EmailResponse emailResponse = await _sendMailService.PaymentVerifiedMailAsync(user, message);
            return new Response { Message = message, Action = "Payment verification", StatusCode = 200, IsEmailSent = emailResponse.Sent};
        }
    }
}
