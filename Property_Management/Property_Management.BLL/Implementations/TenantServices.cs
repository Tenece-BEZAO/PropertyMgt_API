/*using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;



namespace Property_Management.BLL.Implementations
{*/
/*public class TenantServices : ITenantServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRepository<Tenant> _tenantRepo;
   *//* private readonly IRepository<Unit> _unitRepo;
    private readonly IRepository<Lease> _leaseRepo;
    private readonly IRepository<LandLord> _landRepo;
*//*
        public TenantServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();*/
/* _leaseRepo = _unitOfWork.GetRepository<Lease>();
 _unitRepo = _unitOfWork.GetRepository<Unit>();
 _landRepo = _unitOfWork.GetRepository<LandLord>();*/
/* }*/
/*   public async Task<Response> AddTenant(TenantRequest request)
   {
       //Property newProperty = _mapper.Map<Property>(request);
       string TenantId = Guid.NewGuid().ToString();
       Tenant tenant = new()
       {
           TenantId = TenantId,
           UnitId = request.UnitId,
           LastName = request.LastName,
           FirstName = request.FirstName,
           Email = request.Email,
           MoveInDate = request.MoveInDate,
           *//*  Address = request.Address,*//*
           Occupation = request.Occupation,
           MoveOutDate = request.MoveOutDate,
           PhoneNumber = request.PhoneNumber,
           PropertyId = request.PropertytId,
           *//*  LeaseId = request.,*//*

       };

       var unit = await _unitRepo.GetSingleByAsync(l => l.UnitId == request.UnitId);
       if (unit == null)
       {
           throw new InvalidOperationException($"The unit with the id {request.UnitId} was not found.");
       }
       unit.TenantId = TenantId;
       await _tenantRepo.AddAsync(tenant);
       await _unitRepo.UpdateAsync(unit);
       return new Response
       {
           StatusCode = 201,
           Message = "Tenant added successfully",
           Action = "Adding Tenant"
       };
   }
*/
/*public async Task<(bool successful, string msg)> DeleteAsync(string tenantId, string leaseId, string unitId)
{
    // User user = ToDoListDbContext.GetUsersWithToDos().SingleOrDefault(u => u.Id == model.UserId);
    Tenant tenant = await _tenantRepo.GetSingleByAsync(u => u.TenantId == tenantId,
        include: u => u.Include(x => x.Leases.Where(u => u.Upcoming_Tenant == tenantId)), tracking: true);

    if (tenant == null)
    {
        return (false, $"Tenant with id:{tenantId} wasn't found");
    }

    Lease lease = tenant.Leases.FirstOrDefault(u => u.LeaseId == leaseId);

    if (lease != null)
    {
        tenant.Leases.Remove(lease);

        return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Lease with tenant:{lease.Upcoming_Tenant} was deleted") : (false, $"Delete Failed");
    }
    return (false, $"Lease with id:{leaseId} was not found");

    Unit unit = tenant.Leases.FirstOrDefault(u => u.LeaseId == leaseId);

    if (lease != null)
    {
        tenant.Leases.Remove(lease);

        return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Lease with tenant:{lease.Upcoming_Tenant} was deleted") : (false, $"Delete Failed");
    }
    return (false, $"Lease with id:{leaseId} was not found");


    Unit unit = await _unitRepo.GetSingleByAsync(u => u.UnitId == unitId);

}
}*/




/*}
*/

using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

using Property_Management.DAL.Implementations;

namespace Property_Management.BLL.Implementations
{

    public class TenantServices : ITenantServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Tenant> _tenantRepo;

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

    }
}