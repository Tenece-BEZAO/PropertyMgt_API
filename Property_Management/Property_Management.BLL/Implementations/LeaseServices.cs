using AutoMapper;
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
        private readonly IRepository<Property> _propRepo;
        private readonly IRepository<LandLord> _landLordRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISendMailService _sendMailService;

        public LeaseServices(IUnitOfWork unitOfWork, IMapper mapper, ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _leaseRepo = _unitOfWork.GetRepository<Lease>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
            _propRepo = _unitOfWork.GetRepository<Property>();
            _landLordRepo = _unitOfWork.GetRepository<LandLord>();
        }
        public async Task<Response> CreateLease(CreateLeaseRequest request)
        {
            Property property = await _propRepo.GetSingleByAsync(tenant => tenant.PropertyId == request.PropertyId);
            if (property == null)
                throw new InvalidOperationException($"The property with the id {request.PropertyId} was not found.");

            bool leaseExist = await _propRepo.AnyAsync(tenant => tenant.LeaseId == request.PropertyId);
            if (leaseExist)
                throw new InvalidOperationException($"The property with the id {request.PropertyId} was has already been asigned a lease.");

            string leaseId = Guid.NewGuid().ToString();
            Lease lease = new Lease { Id = leaseId };
            Lease newLease = _mapper.Map(request, lease);
            property.LeaseId = leaseId;
            await _leaseRepo.AddAsync(newLease);
            await _propRepo.UpdateAsync(property);
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
                throw new InvalidOperationException($"Lease with the Id {leaseId} was not found.");

            lease.IsDeleted = true;
            await _leaseRepo.UpdateAsync(lease);

            return new Response { Action = "Delete Lease", Message = $"Lease with id {leaseId} has been removed.", StatusCode = 200, };
        }

        public async Task<Response> AcceptOrRejectLease(AcceptLeaseRequest request)
        {
            Lease lease = await _leaseRepo.GetSingleByAsync(lease => lease.Id == request.LeaseId && lease.PropertyId == request.PropertyId);
            Property leasedProperty = await _propRepo.GetSingleByAsync(prop => prop.PropertyId == request.PropertyId);
            if(leasedProperty == null) 
                throw new InvalidOperationException($"The property with {request.PropertyId} was not found");
            if (lease == null)
                throw new InvalidOperationException($"lease with the Id {request.LeaseId} and property Id {request.PropertyId} was not found.");

            Tenant? tenantLeaseExist = await _tenantRepo.GetSingleByAsync(tenant => tenant.LeaseId == lease.Id);
            if (tenantLeaseExist != null)
                throw new InvalidOperationException($"{tenantLeaseExist.FirstName} You have already accepted this lease.");

            Tenant? tenant = await _tenantRepo.GetSingleByAsync(tenant => tenant.TenantId == request.TenantId);
            if (tenant == null)
                throw new InvalidOperationException($"Tenant with the Id {request.TenantId} was not found.");
            
            LandLord? landlord = await _landLordRepo.GetSingleByAsync(l => l.Id == leasedProperty.LandLordId);
            if (landlord == null)
                throw new InvalidOperationException($"Landlord with the Id {leasedProperty.LandLordId} was not found.");

            tenant.LeaseId = request.LeaseId;
            tenant.PropertyId = request.PropertyId;
            await _tenantRepo.UpdateAsync(tenant);

            if (!request.AcceptLease)
            {
            EmailResponse mailResponseNotAccepted = await _sendMailService.LeaseAcceptanceMailAsync(landlord, tenant, leasedProperty, "regected");
                return new Response
                {
                    Action = "Lease acceptance.",
                    Message = "The lease aggrement was regected.",
                    StatusCode = 201,
                    IsEmailSent = mailResponseNotAccepted.Sent
                };
            }

            EmailResponse mailResponseAccepted = await _sendMailService.LeaseAcceptanceMailAsync(landlord, tenant, leasedProperty, "accepted");
            lease.Status = request.AcceptLease;
            await _leaseRepo.UpdateAsync(lease);
            return new Response
            {
                Action = "Lease acceptance.",
                Message = "You have accepted this lease.",
                StatusCode = 201,
                IsEmailSent = mailResponseAccepted.Sent
            };
        }
    }
}
