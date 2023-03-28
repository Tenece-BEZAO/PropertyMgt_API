using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {

        private readonly IUserAuth _userAuth;
        public UserAuthController(IUserAuth userAuth)
        {
            _userAuth = userAuth;
        }


        [AllowAnonymous]
        [HttpPost("Create-user", Name = "Create-New-User")]
        [SwaggerOperation(Summary = "Create user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(SuccessResponse))]
        public async Task<IActionResult> CreateUser(UserRegistrationRequest request)
        {
            request.Role = "user";
            SuccessResponse response = await _userAuth.CreateUserAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("Login-user", Name = "Login-User")]
        [SwaggerOperation(Summary = "Login user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Login successfull", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Login failed.", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(SuccessResponse))]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            AuthenticationResponse response = await _userAuth.LoginUserAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("Recet-password", Name = "Recet-password")]
        [SwaggerOperation(Summary = "Login user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Password recet successfull", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Password recet failed.", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(SuccessResponse))]
        public async Task<IActionResult> RecetPassword(ResetPasswordRequest request)
        {
            AuthenticationResponse response = await _userAuth.ResetPasswordAsync(request);
            return Ok(response);
        }
        
        [AllowAnonymous]
        [HttpPost("Change-password", Name = "Change-password")]
        [SwaggerOperation(Summary = "Login user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Password change successfull", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Password change failed.", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(SuccessResponse))]
        public async Task<IActionResult> ChangePassword(string userId)
        {
            string response = await _userAuth.ChangePassword(userId);
            return Ok(response);
        }


        [HttpPost("Logout", Name = "Logout-user")]
        public async Task<IActionResult> Logout()
        {
            SuccessResponse response = await _userAuth.LogoutAsync();
            return Ok(response);
        }

    }
}
