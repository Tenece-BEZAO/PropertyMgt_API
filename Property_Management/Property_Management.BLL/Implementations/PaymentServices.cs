using AutoMapper;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Services;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;


namespace Property_Management.BLL.Implementations
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Transaction> _transRepo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string? _token;
        private PayStackApi PaystackApi { get; set; }

        public PaymentServices(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _transRepo = _unitOfWork.GetRepository<Transaction>();
            _configuration = configuration;
            _token = _configuration["PayMent:PayStackApiKey"];
            PaystackApi = new PayStackApi(_token);
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

        public async Task<PaymentResponse> MakePayment(PaymentRequest request, string paymentFor)
        {
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
                throw new InvalidOperationException($"Sorry! error occured while performing the transaction {response.Message} . Do try again with valid deatils. ");

            Transaction newPayment = _mapper.Map<Transaction>(request);
            newPayment.TransactionRefereal = transRequest.Reference;
            await _transRepo.AddAsync(newPayment);
            return new PaymentResponse
            {
                Message = $"{request.Name} your Payment link has been generated: {response.Data.AuthorizationUrl} use this link to complete your payment.",
                PaymentFor = $"{paymentFor} payment",
                TransactionAmount = request.Amount,
                PaymentLink = response.Data.AuthorizationUrl,
                ReferenceKey = refernceKey,
            };
        }

        public async Task<Response> VerifyPayment(string reference)
        {
            TransactionVerifyResponse response = PaystackApi.Transactions.Verify(reference);
            if (response.Data.Status != "success")
                throw new InvalidOperationException("Sorry! verification failed. Error occured while trying to verify the transaction. It seems your payment was not successful.");

            var transaction = await _transRepo.GetSingleByAsync(trans => trans.TransactionRefereal == reference);
            if (transaction == null)
                throw new InvalidOperationException($"Transaction was not found. {response.Data.GatewayResponse}");

            transaction.Status = true;
            await _transRepo.UpdateAsync(transaction);
            return new Response { Message = $"Payment verification succesful. {response.Data.GatewayResponse}", Action = "Payment verification", StatusCode = 200 };
        }
    }
}
