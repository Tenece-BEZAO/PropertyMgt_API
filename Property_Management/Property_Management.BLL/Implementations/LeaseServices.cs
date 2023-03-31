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

        public async Task<IEnumerable<TenantResponse>> GetAllRentPaymentDetails()
        {
            return (await _leaseRepo.GetAllAsync(include: u => u.Include(t => t.Tenant))).Select(t => new TenantResponse
            {
               Rent = t.Rent,
               SecurityDeposit = t.SecurityDeposit,
               Tenants = t.Tenant.Select(u => new Tenant
               {
                   FirstName = t.Tenant.FirstOrDefault().FirstName,
                   Email = t.Tenant.FirstOrDefault().Email,
               })
            });
        }

        public async Task<Lease> GetRentPaymentDetails(string tenantId)
        {
            Lease lease = await _leaseRepo.GetSingleByAsync(l => l.TenantId == tenantId);
            if (lease == null)
                throw new InvalidOperationException($"The tenant id {tenantId} does not exist.");
            return lease;
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
