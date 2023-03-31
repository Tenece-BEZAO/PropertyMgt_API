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