using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;


namespace Property_Management.BLL.Implementations
{
   public class MaintenanceRequestServices : IMaintenanceRequestServices
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MaintenanceRequest> _requestRepository;
        private readonly IRepository<Property> _propertyRepository;
        private readonly IRepository<Unit> _unitRepository;

        public MaintenanceRequestServices(IMapper mapper, IRepository<MaintenanceRequest> requestRepository, IRepository<Property> propertyRepository, IRepository<Unit> unitRepository)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
            _propertyRepository = propertyRepository;
            _unitRepository = unitRepository;
        }

        public async Task<Response> CreateMaintenanceRequestAsync(MaintenanceRequestrequests requests)
        {
            // Map request DTO to the maintenance request entity 
            var maintenanceRequest = _mapper.Map<MaintenanceRequest>(requests);

            // Retrieve the property and unit associated with the request and check if they exist 
            var property = await _propertyRepository.GetByIdAsync(requests.PropertyId);
            if (property == null)
            {
                throw new InvalidOperationException($"The Property with the id {requests.PropertyId}was not found.");
            }
            var unit = await _unitRepository.GetByIdAsync(requests.UnitId);
            if (unit == null)
            {
                throw new InvalidOperationException($"The Unit with unit Id {requests.UnitId} was not found");
            }
            maintenanceRequest.Property = property;
            maintenanceRequest.Unit = unit;

            // Save the maintenance request to the database
            await _requestRepository.AddAsync(maintenanceRequest);

            return new Response
            {
                StatusCode = 201,
                Message = "Maintenance request created successfully",
                Action = "Creating maintenance request"
            };
            
        }
    }
    
}