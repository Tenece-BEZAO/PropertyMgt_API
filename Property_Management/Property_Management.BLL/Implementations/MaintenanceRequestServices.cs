//using AutoMapper;
//using Property_Management.BLL.DTOs.Requests;
//using Property_Management.BLL.DTOs.Responses;
//using Property_Management.DAL.Entities;
//using Property_Management.DAL.Implementations;
//using Property_Management.DAL.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Property_Management.BLL.Implementations
//{
//    public class MaintenanceRequestServices
//    {
//        private readonly IMapper _mapper;
//        private readonly IRepository<Tenant> _tenantRepo;
//        private readonly IRepository<Property> _propertyRepo;
//        private readonly IUnitOfWork _unitOfWork;
//        public MaintenanceRequestServices(IMapper mapper)
//        {
//            _mapper = mapper
//            _propertyRepo = _unitOfWork.GetRepository<Property>();
           
//        }
//        public async Task<Response> CreateRequest(MaintenanceRequestRequests request)
//        {
//            var MaintenanceRequest = _mapper.Map<MaintenanceRequestRequests>(request);

//            var propertyToMaintain = await _propertyRepo.GetSingleByAsync(p => p.PropertyId == request.propertyId);
//            if (propertyToMaintain == null)
//            {
//                throw new InvalidOperationException($"The landord with the id {request.propertyId} was not found.");
//            }
//        }
//    }
//}