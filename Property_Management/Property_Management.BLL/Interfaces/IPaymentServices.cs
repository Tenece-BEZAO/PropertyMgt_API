using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IPaymentServices
    {
        Task<PaymentResponse> MakePaymentAsync(PaymentRequest request);
        Task<IEnumerable<TransactionResponse>> GetAllPaymentAsync();
        Task<TransactionResponse> GetPaymentAsync(string Id);
        Task<Response> VerifyPaymentAsync(string reference);
    }
}
