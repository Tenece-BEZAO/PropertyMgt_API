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
        Task<IEnumerable<TenantResponse>> GetAllRentPaymentDetails();
        Task<Lease> GetRentPaymentDetails(string tenantId);
        Task<Response> GetAllSecurityDeposit();
        Task<Response> GetSecurityDeposit(string tenantId);
    }
}
