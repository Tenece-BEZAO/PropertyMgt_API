using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Interfaces
{
    public interface ITenantServices
    {

/*
   
        /*Task<Response> CreateTenant(CreateTenantRequest request);
        Task<Response> UpdateTenant(string tenantId, CreateTenantRequest request);
        Task<Response> RemoveTenant(string tenantId);
        *//*Task<Response> AcceptOrRejectTenant(AcceptTenantRequest request);*//*
        Task<IEnumerable<PaymentInfoResponse>> GetAllRentPaymentDetails();
        Task<IEnumerable<PaymentInfoResponse>> GetTenantWhosRentHasExpired();
        Task<Response> NofityRentExiration(string tenantId);
        Task<IEnumerable<PaymentInfoResponse>> GetTenantWhosPaymentDetailsAreStillUpToDate();
        Task<IEnumerable<Tenant>> GetRentPaymentDetails(string tenantId);
        Task<Response> GetAllSecurityDeposit();
        Task<Response> GetSecurityDeposit(string tenantId);*/


        Task<string> CreateTenant(TenantDTO tenant);
        Task<TenantDTO> EditTenant(string id, TenantDTO tenant);
        Task<TenantDTO> GetTenantById(string tenant);

        Task<IEnumerable<TenantDTO>> GetAllTenants();
        Task<bool> DeleteTenant(string id);
    }
}
