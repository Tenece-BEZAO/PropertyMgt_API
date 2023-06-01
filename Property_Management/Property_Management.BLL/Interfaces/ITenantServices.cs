using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Interfaces
{
    public interface ITenantServices
    {
        void Create(CreateTenantVM model);
        IEnumerable<Tenant> GetTenants();
        Task<IEnumerable<TenantWithMaintenaceVM>> GetTenantsWithMaintenanceAsync();
        Task<Response> CreateMaintenanceRequestAsync(MaintenanceRequestrequests request);
    }

}