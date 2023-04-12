using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IUserAuth
    {
        Task<AuthenticationResponse> CreateUserAsync(UserRegistrationRequest userRegistrationRequest);
        Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest);
        Task<Response> LogoutAsync();
        Task<Response> ToggleUserActivation(string userId);
        Task<Response> ResetPasswordAsync(ResetPasswordRequest request);
        Task<Response> ChangePassword(ChangePasswordRequest changePasswordRequest);
        Task<Response> ChangeEmail(ChangeEmailRequest request);
    }
}
