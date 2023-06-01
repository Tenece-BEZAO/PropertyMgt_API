using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

using Property_Management.DAL.Implementations;
using Azure;
using Property_Management.BLL.DTOs.Requests;
using AutoMapper;

namespace Property_Management.BLL.Implementations
{

    public class TenantServices : ITenantServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IRepository<Property> _propertyRepo;
        private readonly IRepository<MaintenanceRequest> _maintenanceRequest;


        public TenantServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
        }
       
        public void Create(CreateTenantVM model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tenant> GetTenants()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TenantWithMaintenaceVM>> GetTenantsWithMaintenanceAsync()
        {

            return (await _tenantRepo.GetAllAsync(include: u => u.Include(t => t.MaintenanceRequests))).Select(u => new TenantWithMaintenaceVM
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Maintenances = u.MaintenanceRequests.Select(t => new MaintenaceVM
                {
                    MaintenanceRequestId = t.MaintenanceRequestId,
                    UnitId = t.UnitId,
                    Description = t.Description,
                    ReportedTo = t.ReportedTo,
                    Priority = t.Priority.ToString(),
                    LoggedBy = t.LoggedBy,
                    DateLogged = t.DateLogged,
                    DueDate = t.DueDate
                })
            }); ;
        }
        public async Task<Response> MakeMaintenanceRequest(MaintenanceRequestrequests request)
        {
            var maintenanceRequest = _tenantRepo.GetSingleByAsync(m => m.PropertyId == request.PropertyId);
            if (maintenanceRequest == null)
            { 
                throw new InvalidOperationException($"The landord with the id {request.MaintenanceRequestId} was not found.");
            }
            //Perform an operation to make maintenance Request and update to database , this should be an HTTP Put/post/patch method(anyone used for update) 

        }

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