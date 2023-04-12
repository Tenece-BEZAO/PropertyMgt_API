using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Interfaces
{
    public interface ILeaseServices
    {
        Task<Response> CreateLease(CreateLeaseRequest request);
        Task<Response> UpdateLease(string leaseId, CreateLeaseRequest request);
        Task<Response> RemoveLease(string leaseId);
        Task<Response> AcceptOrRejectLease(AcceptLeaseRequest request);
        Task<IEnumerable<PaymentInfoResponse>> GetAllRentPaymentDetails();
        Task<IEnumerable<PaymentInfoResponse>> GetTenantWhosRentHasExpired();
        Task<Response> NofityRentExiration(string tenantId);
        Task<IEnumerable<PaymentInfoResponse>> GetTenantWhosPaymentDetailsAreStillUpToDate();
        Task<IEnumerable<Tenant>> GetRentPaymentDetails(string leaseId);
        Task<Response> GetAllSecurityDeposit();
        Task<Response> GetSecurityDeposit(string tenantId);
    }
}
