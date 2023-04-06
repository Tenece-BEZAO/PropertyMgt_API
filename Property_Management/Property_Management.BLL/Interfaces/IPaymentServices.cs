using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IPaymentServices
    {
        Task<PaymentResponse> MakePayment(PaymentRequest request, string paymentFor);
        Task<IEnumerable<TransactionResponse>> GetAllPayment();
        Task<TransactionResponse> GetPayment(string Id);
        Task<Response> VerifyPayment(string reference);
    }
}
