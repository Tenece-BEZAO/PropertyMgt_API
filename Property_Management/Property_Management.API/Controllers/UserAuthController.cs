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
        [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<IActionResult> CreateUser(UserRegistrationRequest request)
        {
            AuthenticationResponse response = await _userAuth.CreateUserAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("Login-user", Name = "Login-User")]
        [SwaggerOperation(Summary = "Login user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Login successfull", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Login failed.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            AuthenticationResponse response = await _userAuth.LoginUserAsync(request);
            return Ok(response);
        }


        [Authorize]
        [HttpPut("Recet-password", Name = "Recet-password")]
        [SwaggerOperation(Summary = "Login user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Password recet successfull", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Password recet failed.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RecetPassword(ResetPasswordRequest request)
        {
            Response response = await _userAuth.ResetPasswordAsync(request);
            return Ok(response);
        }
        
        [Authorize]
        [HttpPut("Change-email", Name = "Change-email")]
        [SwaggerOperation(Summary = "change user email")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Email changed successfull", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Email change failed.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ChangeEmail(ChangeEmailRequest changeEmailRequest)
        {
            Response response = await _userAuth.ChangeEmail(changeEmailRequest);
            return Ok(response);
        }
        
        [Authorize]
        [HttpPut("Change-password", Name = "Change-password")]
        [SwaggerOperation(Summary = "Change user password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Password change successfull", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Password change failed.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            Response response = await _userAuth.ChangePassword(changePasswordRequest);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("Toggle-user-activation")]
        [SwaggerOperation(Summary = "Change user password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "User activation toggle successful", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "user activation failed.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ToggleUserActiveStatus(string userId)
        {
            Response response = await _userAuth.ToggleUserActivation(userId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("Logout", Name = "Logout-user")]
        [SwaggerOperation(Summary = "Logout user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "You have logged out successfully.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Sorry! error occured while processing your request.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Logout()
        {
            Response response = await _userAuth.LogoutAsync();
            return Ok(response);
        }

    }
}
