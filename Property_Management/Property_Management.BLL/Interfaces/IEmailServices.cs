using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IEmailServices
    {
        Task<SubscriptionResponse> SubscribeNewsletterEmailAsync(string email);
        Task<IEnumerable<FetchSubcribedUserEmailResponse>> GetAllSubscribedEmailAsync();
        Task<FetchSubcribedUserEmailResponse> GetSubscribedEmailAsync(string email);
        Task<EmailResponse> SendMailAsync(EmailRequests mailRequest);
        Task<EmailResponse> SendBulkMailAsync(SendBulkEmailRequest bulkMessageRequest);
    }
}
