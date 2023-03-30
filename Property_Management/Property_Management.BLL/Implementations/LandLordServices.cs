
using AutoMapper;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class LandLordServices : ILandLordServices
    {
        private readonly IRepository<Property> _propRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<LandLord> _landRepo;
        public LandLordServices(IUnitOfWork unitOfWork, IMapper mapper)
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
                OwnedBy = request.OwnedBy,
                Name = request.Name,
                Image = request.Image,
                Price = request.Price,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Zipcode = request.Zipcode,
                NumOfUnits = request.NumOfUnits,

            };

            LandLord newLandLord = new(){ PropertyId = PropertyId };
            var landlord = _landRepo.GetSingleByAsync();
            await _propRepo.AddAsync(property);
            await _landRepo.UpdateAsync(newLandLord);
            return new Response
            {
                StatusCode = 201,
                Message = "Property added successfully",
                Action = "Adding property"
            };
        }
    }
}
