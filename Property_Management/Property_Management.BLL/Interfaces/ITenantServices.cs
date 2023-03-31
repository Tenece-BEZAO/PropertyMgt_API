
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;

using System.Security.Cryptography;

namespace Property_Management.BLL.Interfaces
{
    public interface ITenantServices
    {
        void Create(CreateTenantVM model);
        IEnumerable<Tenant> GetTenants();
        Task<IEnumerable<TenantWithMaintenaceVM>> GetTenantsWithMaintenanceAsync();

    }
}
