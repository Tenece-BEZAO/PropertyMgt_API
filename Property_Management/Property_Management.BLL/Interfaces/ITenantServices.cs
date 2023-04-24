using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Interfaces
{
    public interface ITenantServices
    {
        Task<AuthenticationResponse> CreateTenant(UserRegistrationRequest request);
        Task<TenantDTO> UpdateTenant(TenantDTO tenantDto);
        Task<Response> DeleteTenant(string tenantId);
        Task<IEnumerable<TenantDTO>> GetAllTenants();
        Task<TenantDTO> GetTenantById(string tenant);
        Task<Response> AcceptOrRejectTenant(AcceptTenantRequest request);
        Task<IEnumerable<PaymentInfoResponse>> GetAllRentPaymentDetails();
        Task<IEnumerable<PaymentInfoResponse>> GetTenantWhosRentHasExpired();
        Task<Response> NofityRentExiration(string leaseId);
        Task<IEnumerable<PaymentInfoResponse>> GetTenantWhosPaymentDetailsAreStillUpToDate();
        Task<IEnumerable<Tenant>> GetRentPaymentDetails(string tenantId);
        Task<Response> GetAllSecurityDeposit();
        Task<Response> GetSecurityDeposit(string tenantId);
    }
}
