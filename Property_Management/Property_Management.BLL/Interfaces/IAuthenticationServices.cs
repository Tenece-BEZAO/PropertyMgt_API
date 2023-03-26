using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;

namespace Property_Management.BLL.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<string> CreateUser(UserRegistrationRequest request);
        Task<AuthenticationResponse> UserLogin(LoginRequest request);
        Task<string> ChangePassword(string userId, ChangePasswordRequest request);
        Task<string> ResetPassword(ResetPasswordRequest request);
        // Task<AuthenticationResponse> ConfirmTwoFactorToken(TwoFactorLoginRequest request);
        Task<string> VerifyUser(VerifyAccountRequest request);
        Task<string> ChangeEmail(ChangeEmailRequest request);
        Task ToggleUserActivation(string userId);
        Task UpdateRecoveryEmail(string userId, string email);
        //Task<UserResponse> GetUser(string userId);
        //Task<AuthenticationResponse> ImpersonateUser(ImpersonationLoginRequest request);
        //Task<ImpersonationLoginResponse> ImpersonationLogin(ImpersonationLoginRequest request);
    }
}
