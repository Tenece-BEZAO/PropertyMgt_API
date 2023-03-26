using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs;

namespace Property_Management.BLL.Interfaces
{
    public interface IUserAuth
    {
        Task<Status> CreateUserAsync(UserRegistrationRequest userRegistrationRequest);
        Task<Status> LoginUserAsync(LoginRequest loginRequest);
        Task<Status> LogoutAsync();
    }
}
