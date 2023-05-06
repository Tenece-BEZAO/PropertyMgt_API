

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class TenantServices : ITenantServices
    {

        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IRepository<Lease> _leaseRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISendMailService _sendMailService;
        private readonly IUserAuth _userAuth;


        public TenantServices(IUnitOfWork unitOfWork, IMapper mapper, ISendMailService sendMailService, IUserAuth userAuth)
        {
            _sendMailService = sendMailService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
            _leaseRepo = _unitOfWork.GetRepository<Lease>();
            _mapper = mapper;
            _userAuth = userAuth;
        }

        public Task<Response> AcceptOrRejectTenant(AcceptTenantRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<EmailResponse> CreateTenant(UserRegistrationRequest request)
        {
           return await _userAuth.CreateUserAsync(request);
        }


        public async Task<Response> DeleteTenant(string id)
        {
            Tenant tenant = await _tenantRepo.GetByIdAsync(id);
            if (tenant == null) throw new InvalidOperationException($"Tenant with the id {id} was not found.");

            tenant.IsDeleted = true;
             await _tenantRepo.UpdateAsync(tenant);
            return new Response { Action = "Delete tenant", StatusCode = 200, Message = $"Tenant with email. {tenant.Email} has been deleted." };
        }

        public async Task<TenantResponse> UpdateTenant(TenantResponse tenantDto)
        {
            var tenant = await _tenantRepo.GetByIdAsync(tenantDto.TenantId);

            if (tenant == null)
                throw new InvalidOperationException($"Tenant with the id {tenantDto.TenantId} was not found.");

            return TenantResponse.FromTenant(tenantDto);
        }

        public async Task<IEnumerable<TenantResponse>> GetAllTenants()
        {
            return (await _tenantRepo.GetAllAsync(include: u => u.Include(t => t.Lease))).Select(tenant => new TenantResponse
            {
                TenantId = tenant.TenantId,
                UserId = tenant.UserId,
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                Address = tenant.Address,
                Email = tenant.Email,
                Occupation = tenant.Occupation,
                PhoneNumber = tenant.PhoneNumber,
                MoveInDate = tenant.MoveInDate,
                MoveOutDate = tenant.MoveOutDate,
                Leases = tenant.Lease.Select(l => new LeaseDto
                {
                    LeaseId = l.Id,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Description = l.Description,
                }),
            });
        }

        public async Task<TenantResponse> GetTenantById(string id)
        {
            var tenant = await _tenantRepo.GetSingleByAsync(t => t.TenantId == id);
            if (tenant == null) throw new InvalidOperationException($"Tenant with the id: {id} was not found.");

           return _mapper.Map<TenantResponse>(tenant);
        }


        public async Task<IEnumerable<PaymentInfoResponse>> GetAllRentPaymentDetails()
        {
            return (await _leaseRepo.GetAllAsync(include: u => u.Include(t => t.Tenant))).Select(l => new PaymentInfoResponse
            {
                Rent = l.Rent,
                SecurityDeposit = l.SecurityDeposit,
                TenantResponse = new TenantPaymentInfoResponse
                {
                    UserId = l.Tenant.UserId,
                    FullName = $"{l.Tenant.FirstName} {l.Tenant.LastName}",
                    Address = l.Tenant.Address,
                    Phone = l.Tenant.PhoneNumber,
                }
            });
        }

        public async Task<IEnumerable<PaymentInfoResponse>> GetTenantWhosPaymentDetailsAreStillUpToDate()
        {
            var paymentInfos = (await _leaseRepo.GetByAsync(
                predicate: c => c.Rent > decimal.Zero && c.EndDate <= DateTime.Now && c.Status,
                orderBy: t => t.OrderBy(tenant => tenant.StartDate),
                include: u => u.Include(t => t.Tenant))).Select(l => new PaymentInfoResponse
                {
                    Rent = l.Rent,
                    SecurityDeposit = l.SecurityDeposit,
                    TenantResponse = new TenantPaymentInfoResponse
                    {
                        UserId = l.Tenant.UserId,
                        FullName = $"{l.Tenant.FirstName} {l.Tenant.LastName}",
                        Address = l.Tenant.Address,
                        Phone = l.Tenant.PhoneNumber,
                    }
                });

            if (paymentInfos == null) throw new Exception("No item found.");

            return paymentInfos;
        }

        public async Task<IEnumerable<PaymentInfoResponse>> GetTenantWhosRentHasExpired()
        {

            IEnumerable<PaymentInfoResponse> paymentDetails = (await _leaseRepo.GetByAsync(
                predicate: c => c.Rent < decimal.One && c.EndDate < DateTime.Now && c.Status,
                orderBy: t => t.OrderBy(tenant => tenant.StartDate),
                include: u => u.Include(t => t.Tenant))).Select(l => new PaymentInfoResponse
                {
                    MovinDate = l.StartDate.ToLongDateString(),
                    EndDate = l.EndDate.ToLongDateString(),
                    Rent = l.Rent,
                    SecurityDeposit = l.SecurityDeposit,
                    TenantResponse = new TenantPaymentInfoResponse
                    {
                        UserId = l.Tenant.UserId,
                        FullName = $"{l.Tenant.FirstName} {l.Tenant.LastName}",
                        Address = l.Tenant.Address,
                        Phone = l.Tenant.PhoneNumber,
                    }
                });

            if (paymentDetails == null) throw new Exception("No item was found.");

            return paymentDetails;
        }



        public async Task<Tenant> GetRentPaymentDetails(string leaseId)
        {
            Tenant tenantDetails = await _tenantRepo.GetSingleByAsync(t => t.LeaseId == leaseId);
            if (tenantDetails == null)
                throw new InvalidOperationException($"The tenant with lease id {leaseId} does not exist.");
            return tenantDetails;
        }

        public Task<Response> GetSecurityDeposit(string tenantId)
        {
            throw new NotImplementedException();
        }
        public async Task<Response> NofityRentExiration(string tenantId)
        {
            Tenant rentExpiredTenant = await _tenantRepo.GetSingleByAsync(predicate: t => t.TenantId == tenantId, include: p => p.Include(tenant => tenant.Payments));
            if (rentExpiredTenant == null)
                throw new InvalidOperationException($"Sorry!. tenant with the Id {tenantId} does't exist.");

            Payment? tenantPaymentDetail = rentExpiredTenant.Payments.FirstOrDefault(te => te.PaymentDate <= DateTime.UtcNow);
            if (tenantPaymentDetail == null)
            {
                EmailResponse mailResponse = await _sendMailService.RentExpireMailAsync(rentExpiredTenant, "Your rent is still active.", false);
                return new Response { Action = "Rent Expire alert", StatusCode = 200, Message = "Your rent is still active.", IsEmailSent = true};
            }
            EmailResponse mailResponse2 = await _sendMailService.RentExpireMailAsync(rentExpiredTenant, "Your rent has expired.", true);
            return new Response { Action = "Rent Expire alert", StatusCode = 200, Message = "Your rent has expired." };

        }

        Task<IEnumerable<Tenant>> ITenantServices.GetRentPaymentDetails(string tenantId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetAllSecurityDeposit()
        {
            throw new NotImplementedException();
        }
    }
}
