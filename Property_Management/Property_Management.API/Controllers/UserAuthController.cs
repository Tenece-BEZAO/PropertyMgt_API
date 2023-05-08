using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

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
        [HttpPost("create-user", Name = "create-new-user")]
        [SwaggerOperation(Summary = "Create user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<IActionResult> CreateUser(UserRegistrationRequest request)
        {
            EmailResponse response = await _userAuth.CreateUserAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("login-user", Name = "login-User")]
        [SwaggerOperation(Summary = "Login user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Login successfull", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Login failed.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            AuthenticationResponse response = await _userAuth.LoginUserAsync(request);
            return Ok(response);
        }


        [Authorize]
        [HttpGet("get-user-profile", Name = "get-user-profile")]
        [SwaggerOperation(Summary = "get user profile details")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "profile fetched successfull", Type = typeof(ProfileResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "fetch failed.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetUserProfileDetails(string email)
        {
            ProfileResponse response = await _userAuth.GetTenantProfileDetails(email);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email", Name = "confirm-email")]
        [SwaggerOperation(Summary = "verify user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Verification successfull", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "verification failed.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> VerifyUser(string id, string token)
        {
            Response response = await _userAuth.ConfirmEmailAsync(id, token);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("activate-2FA", Name = "Activate-two-factor-auth")]
        [SwaggerOperation(Summary = "Activate two factor authentication")]
        [
            SwaggerResponse(StatusCodes.Status200OK,
            Description = "Two factor authentication was updated for {user.UserName}",
            Type = typeof(AuthenticationResponse))
        ]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User with the id {userId} was not found.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> TwoFaLogin(string userId)
        {
            Response response = await _userAuth.Toggle2FAAuthAsync(userId);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("2falogin", Name = "Two factor authentication")]
        [SwaggerOperation(Summary = "Login user with 2FA")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Login successfull", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Authentication failed.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> TwoFaLogin(TwoFactorLoginRequest request)
        {
            AuthenticationResponse response = await _userAuth.TwoFactorLoginAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpGet("forgot-password", Name = "Forgot-password")]
        [SwaggerOperation(Summary = "Forgot user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Check your email we have sent you a link to recet your password.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            Response response = await _userAuth.ForgortPasswordAsync(email);
            return Ok(response);
        }



        [AllowAnonymous]
        [HttpPut("recet-password", Name = "recet-password")]
        [SwaggerOperation(Summary = "Recet user password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Password recet successfull", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Password recet failed.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RecetPassword(ResetPasswordRequest request)
        {
            Response response = await _userAuth.ResetPasswordAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpGet("change-email-request", Name = "change-email-request")]
        [SwaggerOperation(Summary = "Request change email to generate token for changing the user email.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Token to change email was generated successfully.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User was not found.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ChangeEmailRequest(string userId, string newEmail)
        {
            Response response = await _userAuth.ChangeEmailRequestAsync(userId, newEmail);
            return Ok(response);
        }


        [Authorize]
        [HttpPut("change-email", Name = "change-email")]
        [SwaggerOperation(Summary = "change user email")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Email changed successfull", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Email change failed.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ChangeEmail(ChangeEmailRequest changeEmailRequest)
        {
            Response response = await _userAuth.ChangeEmail(changeEmailRequest);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("change-password", Name = "change-password")]
        [SwaggerOperation(Summary = "Change user password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Password change successfull", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Password change failed.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            Response response = await _userAuth.ChangePassword(changePasswordRequest);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("toggle-user-activation")]
        [SwaggerOperation(Summary = "Change user password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "User activation toggle successful", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "user activation failed.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ToggleUserActiveStatus(string userId)
        {
            Response response = await _userAuth.ToggleUserActivation(userId);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("logout-user", Name = "logout-user")]
        [SwaggerOperation(Summary = "Logout user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "You have logged out successfully.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Sorry! error occured while processing your request.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Logout()
        {
            Response response = await _userAuth.LogoutAsync();
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpGet("google-login", Name = "google-login")]
        [SwaggerOperation(Summary = "Login user with their google account.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "User sign-in successfully.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Sorry! error occured while processing your request.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GoogleLogin()
        {
            Response response = await _userAuth.GoogleLoginAsync();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("facebook-login", Name = "facebook-login")]
        [SwaggerOperation(Summary = "Login in user with their facebook account.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "User sign-in successfully.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Sorry! error occured while processing your request.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> FaceBookLogin()
        {
            Response response = await _userAuth.FaceBookLoginAsync();
            return Ok(response);
        }

    }
    //return AccessDenied();
}
