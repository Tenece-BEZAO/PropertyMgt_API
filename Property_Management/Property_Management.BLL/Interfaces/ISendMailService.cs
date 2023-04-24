using Property_Management.BLL.DTOs.Responses;
using Property_Management.DAL.Entities;

namespace Property_Management.DAL.Interfaces
{
    public interface ISendMailService
    {
        Task<EmailResponse> LeaseAcceptanceMailAsync(LandLord landlord, Tenant tenant, Property property, string message);
        Task<EmailResponse> RentExpireMailAsync(Tenant tenant, string message, bool expiredRent);
        Task<EmailResponse> UserCreatedMailAsync(ApplicationUser user);
        Task<EmailResponse> RecetPasswordSuccessMailAsync(ApplicationUser user, string message);
        Task<EmailResponse> PaymentVerifiedMailAsync(ApplicationUser user, string message);
    }
}
