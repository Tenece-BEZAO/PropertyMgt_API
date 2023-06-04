using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class ManagerServices : IManagerServices
    {
        private readonly IPaymentServices _paymentServices;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendMailService _sendMailServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<LandLord> _landRepo;
        private readonly IRepository<Property> _propRepo;
        private readonly IRepository<Unit> _unitRepo;
        private readonly IRepository<Payment> _paymentRepo;
        private readonly IRepository<Lease> _leaseRepo;
        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IRepository<Transaction> _transRepo;
        public ManagerServices(IUnitOfWork unitOfWork, IMapper mapper, IPaymentServices paymentServices, UserManager<ApplicationUser> userManager, ISendMailService sendMailServices)
        {
            _mapper = mapper;
            _paymentServices = paymentServices;
            _sendMailServices = sendMailServices;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _propRepo = _unitOfWork.GetRepository<Property>();
            _landRepo = _unitOfWork.GetRepository<LandLord>();
            _unitRepo = _unitOfWork.GetRepository<Unit>();
            _paymentRepo = _unitOfWork.GetRepository<Payment>();
            _leaseRepo = _unitOfWork.GetRepository<Lease>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
            _transRepo = _unitOfWork.GetRepository<Transaction>();
        }
        

        public async Task<Response> AddProperty(AddOrUpdatePropertyRequest request)
        {
            Property newProperty = _mapper.Map<Property>(request);

            var landlord = await _landRepo.GetSingleByAsync(l => l.Id == request.LandLordId);

            if (landlord == null)
            {
                throw new InvalidOperationException($"The landord with the id {request.LandLordId} was not found.");
            }
            landlord.PropertyId = newProperty.PropertyId;
            await _propRepo.AddAsync(newProperty);
            await _landRepo.UpdateAsync(landlord);
            return new Response
            {
                StatusCode = 201,
                Message = "Property added successfully",
                Action = "Adding property"
            };
        }


        public async Task<PropertyResponse> GetProperty(string propertyId)
        {
            Property fetchedPropWithLease = await _propRepo.GetSingleByAsync(predicate: d => d.PropertyId == propertyId, include: prop => prop.Include(p => p.Lease));
            Property fetchedProp = await _propRepo.GetSingleByAsync(predicate: d => d.PropertyId == propertyId);
            if (fetchedProp == null)
            {
                throw new InvalidOperationException($"Property {propertyId} was not found");
            }

            if(fetchedPropWithLease == null)
            {
                return new PropertyResponse
                {
                    PropertyId = fetchedProp.PropertyId,
                    LandLordId = fetchedProp.LandLordId,
                    Name = fetchedProp.Name,
                    Price = fetchedProp.Price,
                    Image = fetchedProp.Image,
                    Status = fetchedProp.Status,
                    IsDeleted = fetchedProp.IsDeleted,
                };
            }

            return new PropertyResponse 
            {
                Lease = new LeaseResponse
                {
                    LeaseId = fetchedPropWithLease.Lease.Id,
                    Description = fetchedPropWithLease.Lease.Description,
                    StartDate = fetchedPropWithLease.Lease.StartDate,
                    EndDate = fetchedPropWithLease.Lease.EndDate,
                },
                PropertyId = fetchedPropWithLease.PropertyId,
                LandLordId = fetchedPropWithLease.LandLordId,
                Name = fetchedPropWithLease.Name,
                Price = fetchedPropWithLease.Price,
                Image = fetchedPropWithLease.Image,
                Status = fetchedPropWithLease.Status, 
                IsDeleted = fetchedPropWithLease.IsDeleted,
            };
          
         }  
        
        public async Task<Response> DeleteProperty(string propertyId)
        {
            var PropertyToBeDeleted = await _propRepo.GetSingleByAsync(d => d.PropertyId == propertyId);
            if (PropertyToBeDeleted == null)
            {
                throw new InvalidOperationException($"Property {propertyId} was not found");
            }
            var property = await _propRepo.GetSingleByAsync(l => l.PropertyId == propertyId);
            if (property == null)
                throw new InvalidOperationException($"Landlord with Property ID [{propertyId}] was not found.");

            property.IsDeleted = true;
          await _propRepo.UpdateAsync(property);

            return new Response
            {
                StatusCode = 201,
                Message = "Property Deleted successfully",
                Action = "Deleting a property"
            };
         }


        public async Task<Response> UpdateProperty(string propertyId, AddOrUpdatePropertyRequest request)
        {
            var propertyToBeUpdated = await _propRepo.GetSingleByAsync(u => u.PropertyId == propertyId, tracking: true);
            if (propertyToBeUpdated == null)
            {
                throw new InvalidOperationException($"Property {propertyId} was not Found");
            }
            _mapper.Map(request, propertyToBeUpdated);

            await _propRepo.UpdateAsync(propertyToBeUpdated);

            return new Response
            {
                StatusCode = 201,
                Message = "Property updated successfully",
                Action = "Updating a property"
            };
        }

        public async Task<PropertyResponse> RentPropAsync(RentPropRequest request)
        {
            string tenantId = Guid.NewGuid().ToString();
            Tenant checkTenant = await _tenantRepo.GetSingleByAsync(t => t.UserId == request.UserId);
            if (checkTenant == null)
            {
            ApplicationUser user = await _userManager.FindByIdAsync(request.UserId);
                    if (user == null) throw new InvalidOperationException($"Sorry! user with the Id: {request.UserId} was not found.");

                Tenant newTenant = new Tenant
                {
                    TenantId = tenantId,
                    UserId = user.Id,
                    FirstName = user.UserName,
                    LastName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };
                await _tenantRepo.AddAsync(newTenant);
            }

            Unit unit = await _unitRepo.GetSingleByAsync(predicate: u => u.UnitId == request.UnitId && u.PropertyId == request.PropertyId);
            if(unit == null)
                throw new InvalidOperationException($"Unit with Id: {request.UnitId} was not found.");

            Lease lease = await _leaseRepo.GetSingleByAsync(predicate: le => le.Id == request.LeaseId);
            if (lease == null)
                throw new InvalidOperationException($"Lease with Id: {request.LeaseId} was not found.");

            Property property = await _propRepo.GetSingleByAsync(predicate: p => p.PropertyId == request.PropertyId);
            if (property == null)
                throw new InvalidOperationException($"Lease with property Id: {request.PropertyId} was not found.");
            
            LandLord landLord = await _landRepo.GetSingleByAsync(predicate: l => l.Id == property.LandLordId);
            if (landLord == null)
                throw new InvalidOperationException($"Landlord with Property Id: {request.PropertyId} was not found.");

            if (request.Price < property.Price)
                throw new InvalidOperationException($"Sorry! {request.Price} is equal to the price ({property.Price}) of this property.");

            Tenant newCreatedTenant = await _tenantRepo.GetSingleByAsync(t => t.TenantId == tenantId);
                    if (newCreatedTenant == null) Console.WriteLine($"Sorry! tenant with the Id: {tenantId} was not found, while trying to access a tenant created at runtime while puchasing a property.");

            Tenant tenant = checkTenant ?? newCreatedTenant;


           /* Payment payment = await _paymentRepo.GetSingleByAsync(p => p.Id == request.PaymentId);
            if (payment == null)
                throw new InvalidOperationException($"{tenant.FirstName} has not done any payment yet.");*/

            bool isTransaction = await _transRepo.AnyAsync(trans => trans.Email == tenant.Email && trans.Status == true);
            if (!isTransaction)
                throw new InvalidOperationException($"{tenant.FirstName} has not done any transaction yet.");

            tenant.PropertyId = property.PropertyId;
            await _tenantRepo.UpdateAsync(tenant);

            lease.PropertyId = property.PropertyId;
            lease.UnitId = unit.UnitId;
            await _leaseRepo.UpdateAsync(lease);
            EmailResponse emailResponse = await _sendMailServices.SendRentedPropEmailAsync(tenant, property, unit);
            return new PropertyResponse
            {
                Lease = new LeaseResponse
                {
                    LeaseId = lease.Id,
                    Description = lease.Description,
                    StartDate = lease.StartDate,
                    EndDate = lease.EndDate,
                },
                PropertyId = property.PropertyId,
                LandLordId = property.LandLordId,
                Name = property.Name,
                Price = property.Price,
                Image = property.Image,
                Status = property.Status,
                IsDeleted = property.IsDeleted,
            };
        }


        public async Task<IEnumerable<Property>> GetAllProperties()
        {
            var properties = await _propRepo.GetAllAsync();
            if (properties == null) throw new InvalidOperationException("Error occured. Do try again.");
            return properties;
        }


        public async Task<IEnumerable<Property>> GetAllAvaliableOrUnavialbleProperties(bool isAvailable)
        {
            var avaliableProps = await _propRepo.GetByAsync(p => p.Status == isAvailable);
            if (avaliableProps == null) throw new InvalidOperationException("Property not Found. Please try again");
            return avaliableProps;
        }


        public async Task<IEnumerable<Property>> GetAllRentedOrNonRentedPropertiesByLandord(string landlordId, bool condiction)
        {
            var RentedPropsownedByLandLord = await _propRepo.GetByAsync(v => v.LandLordId == landlordId);
            if (RentedPropsownedByLandLord == null) throw new Exception("Landlord with this Id does not own any property.");

            var RentedPropsByLandord = await _propRepo.GetByAsync(p => p.Status == condiction && p.LandLordId == landlordId);
            if (RentedPropsByLandord == null) throw new InvalidOperationException("Sorry! an error occured.");
            return RentedPropsByLandord;
        }
    }
}
