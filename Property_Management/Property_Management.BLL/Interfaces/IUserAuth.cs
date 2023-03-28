using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IUserAuth
    {
        Task<SuccessResponse> CreateUserAsync(UserRegistrationRequest userRegistrationRequest);
        Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest);
        Task<SuccessResponse> LogoutAsync();
        Task<string> ToggleUserActivation(string userId);
        Task<string> ResetPasswordAsync(ResetPasswordRequest request);
        Task<string> ChangePassword(string userId);
    }
}
