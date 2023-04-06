using AutoMapper;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class ManagerServices : IManagerServices
    {
        private readonly IRepository<Property> _propRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<LandLord> _landRepo;
        public ManagerServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _propRepo = _unitOfWork.GetRepository<Property>();
            _landRepo = _unitOfWork.GetRepository<LandLord>();
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


        public async Task<Response> DeleteProperty(string propertyId)
        {
            var PropertyToBeDeleted = await _propRepo.GetSingleByAsync(d => d.PropertyId == propertyId);
            if (PropertyToBeDeleted == null)
            {
                throw new InvalidOperationException($"Property {propertyId} was not found");
            }
            var landlord = await _propRepo.GetSingleByAsync(l => l.PropertyId == propertyId);
            if (landlord == null)
                throw new InvalidOperationException($"Landlord with Property ID [{propertyId}] was not found.");

          await _propRepo.DeleteAsync(landlord);

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
