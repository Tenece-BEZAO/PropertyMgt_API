using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IUserAuth
    {
        Task<EmailResponse> CreateUserAsync(UserRegistrationRequest userRegistrationRequest);
        Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest);
        Task<Response> Toggle2FAAuthAsync(string userId);
        Task<AuthenticationResponse> TwoFactorLoginAsync(TwoFactorLoginRequest request);
        Task<ProfileResponse> GetTenantProfileDetails(string email);
        Task<Response> LogoutAsync();
        Task<Response> ToggleUserActivation(string userId);
        Task<Response> ConfirmEmailAsync(string id, string token);
        Task<Response> ResetPasswordAsync(ResetPasswordRequest request);
        Task<Response> ForgortPasswordAsync(string email);
        Task<Response> ChangePassword(ChangePasswordRequest changePasswordRequest);
        Task<Response> ChangeEmailRequestAsync(string userId, string newEmail);
        Task<Response> ChangeEmail(ChangeEmailRequest request);
        Task<Response> GoogleLoginAsync();
        Task<Response> FaceBookLoginAsync();
    }
}
