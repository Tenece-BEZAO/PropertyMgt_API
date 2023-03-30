
using AutoMapper;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class ManagerServices : IMangerServices
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

        public async Task<Response> AddProperty(AddPropertyRequest request)
        {
            //Property newProperty = _mapper.Map<Property>(request);
            string PropertyId = Guid.NewGuid().ToString();
            Property property = new()
            {
                PropertyId = PropertyId,
                LandLordId = request.OwnedBy,
                Name = request.Name,
                Image = request.Image,
                Price = request.Price,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Zipcode = request.Zipcode,
                NumOfUnits = request.NumOfUnits,

            };

            var landlord = await _landRepo.GetSingleByAsync(l => l.LandLordId == request.OwnedBy);
            if (landlord == null)
            {
                throw new InvalidOperationException($"The landord with the id {request.OwnedBy} was not found.");
            }
            landlord.PropertyId = PropertyId;
            await _propRepo.AddAsync(property);
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
            var PropertyToBeDeleted = _propRepo.GetSingleByAsync(d => d.PropertyId == propertyId);
            if (PropertyToBeDeleted == null)
            {
                throw new InvalidOperationException($"Property {propertyId} was not found");
            }
            await _propRepo.DeleteAsync(PropertyToBeDeleted);
            await _landRepo.UpdateAsync(_landRepo);


            return new Response
            {
                StatusCode = 201,
                Message = "Property Deleted successfully",
                Action = "Deleting a property"
            };


        }
    }
}
