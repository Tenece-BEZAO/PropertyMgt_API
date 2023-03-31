
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
           Property newProperty = _mapper.Map<Property>(request);


            var landlord = await _landRepo.GetSingleByAsync(l => l.LandLordId == request.OwnedBy);
            if (landlord == null)
            {
                throw new InvalidOperationException($"The landord with the id {request.OwnedBy} was not found.");
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
        public async Task<Response> DeleteProperty(DeletePropertyRequest request)
        {
            var PropertyToBeDeleted = await _propRepo.GetSingleByAsync(d => d.PropertyId == request.PropertyId, tracking: true);
            if (PropertyToBeDeleted == null)
            {
                throw new InvalidOperationException($"Property {request.PropertyId} was not found");
            }
            var landlord = await _propRepo.GetSingleByAsync(l => l.PropertyId == request.PropertyId);
            if (landlord != null)
            {
                landlord.PropertyId = null;
                await _propRepo.UpdateAsync(landlord);
            }


            return new Response
            {
                StatusCode = 201,
                Message = "Property Deleted successfully",
                Action = "Deleting a property"
            };
         }
        public async Task<Response> UpdateProperty(UpdatePropertyRequests request)
        {
            var PropertyToBeupdated = await _propRepo.GetSingleByAsync(u => u.PropertyId == request.PropertyId, tracking: true);
            if (PropertyToBeupdated == null)
            {
                throw new InvalidOperationException($"Property {request.PropertyId} was not found");
            }
            var landlord = await _propRepo.Get
        }
                
    }
}
