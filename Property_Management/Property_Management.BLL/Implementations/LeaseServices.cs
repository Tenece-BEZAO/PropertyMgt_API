using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class LeaseServices : ILeaseServices
    {
        private readonly IRepository<Lease> _leaseRepo;
        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LeaseServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _leaseRepo = _unitOfWork.GetRepository<Lease>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
        }
        public async Task<Response> CreateLease(CreateLeaseRequest request)
        {
            Tenant tenant = await _tenantRepo.GetSingleByAsync(tenant => tenant.TenantId == request.TenantId);
            if (tenant == null)
                throw new InvalidOperationException($"Tenant with the id {request.TenantId} was not found.");

            var newLease = _mapper.Map<Lease>(request);
           await _leaseRepo.AddAsync(newLease);

            return new Response
            {
                Action = "Lease Creation.",
                Message = "Your lease has been created.",
                StatusCode = 201,
            };
        }

        public async Task<Response> UpdateLease(string leaseId, CreateLeaseRequest request)
        {
            Lease lease = await _leaseRepo.GetSingleByAsync(lease => lease.Id == leaseId);
            if (lease == null)
                throw new InvalidOperationException($"Lease with id {leaseId} was not found. try again!");
            Lease updatedLease = _mapper.Map(request, lease);
            if (updatedLease == null)
                   throw new InvalidOperationException("The update failed. try again.");

               await _leaseRepo.UpdateAsync(updatedLease);
            return new Response { Action = "Delete Lease", Message = $"Lease with id {leaseId} has been updated.", StatusCode = 200, };
        }

        public async Task<Response> RemoveLease(string leaseId)
        {
            Lease lease = await _leaseRepo.GetSingleByAsync(lease => lease.Id == leaseId);
            if (lease == null)
                throw new InvalidOperationException($"Lease with the ID {leaseId} was not found.");

           await _leaseRepo.DeleteByIdAsync(leaseId);

            return new Response { Action = "Delete Lease", Message = $"Lease with id {leaseId} has been removed.", StatusCode = 200, };
        }

        public async Task<Response> AcceptOrRejectLease(AcceptLeaseRequest request)
        {
            Lease lease = await _leaseRepo.GetSingleByAsync(lease => lease.Id == request.LeaseId && lease.PropertyId == request.PropertyId);
            if (lease == null)
                throw new InvalidOperationException($"lease with the Id {request.LeaseId} and property Id {request.PropertyId} was not found.");

            lease.Status = !lease.Status;
            await _leaseRepo.UpdateAsync(lease);

            return new Response
            {
                Action = "Lease acceptance.",
                Message = "Your have accepted this lease.",
                StatusCode = 201,
            };
        }

        public async Task<IEnumerable<PaymentInfoResponse>> GetAllRentPaymentDetails()
        {
            return (await _leaseRepo.GetAllAsync(include: u => u.Include(t => t.Tenant))). Select(l => new PaymentInfoResponse
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
                predicate: c => c.Rent < decimal.Zero && c.EndDate <= DateTime.Now && c.Status,
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

            if (paymentDetails == null) throw new Exception("No item was found.");

            return paymentDetails;
        }

        public async Task<Response> NofityRentExiration(string tenantId)
        {
            Tenant rentExpiredTenant = await _tenantRepo.GetSingleByAsync(predicate: t => t.TenantId == tenantId, include: p => p.Include(tenant => tenant.Payments));
            if (rentExpiredTenant == null)
                throw new InvalidOperationException($"Sorry!. tenant with the Id {tenantId} does't exist.");

            Payment? tenantPaymentDetail = rentExpiredTenant.Payments.FirstOrDefault(te => te.PaymentDate <= DateTime.UtcNow);
            if (tenantPaymentDetail == null)
            {
                return new Response { Action = "Rent Expire alert", StatusCode = 200, Message = "Your rent is still active." };
            }
                return new Response { Action = "Rent Expire alert", StatusCode = 200, Message = "Your rent has expired." };

        }

        public async Task<IEnumerable<Tenant>> GetRentPaymentDetails(string leaseId)
        {
            IEnumerable<Tenant> tenantDetails = await _tenantRepo.GetByAsync(t => t.LeaseId  == leaseId);
            if (tenantDetails == null)
                throw new InvalidOperationException($"The tenant with lease id {leaseId} does not exist.");
            return tenantDetails;
        }

        public Task<Response> GetAllSecurityDeposit()
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetSecurityDeposit(string tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
