using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Interfaces
{
    public interface ITenantServices
    {
        void Create(CreateTenantVM model);
        IEnumerable<Tenant> GetTenants();
        Task<IEnumerable<TenantWithMaintenaceVM>> GetTenantsWithMaintenanceAsync();

    }
}