using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {

        private readonly IUserAuth _userAuth;
        private readonly JwtGenToken _jwtGenToken = new();
        public UserAuthController(IUserAuth userAuth)
        {
            _userAuth = userAuth;
        }


        [AllowAnonymous]
        [HttpPost("Create-user", Name = "Create-New-User")]
        [SwaggerOperation(Summary = "Create user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(Status))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Status))]
        public async Task<IActionResult> CreateUser(UserRegistrationRequest request)
        {
            request.Role = "user";
            Status response = await _userAuth.CreateUserAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("Login-user", Name = "Login-User")]
        [SwaggerOperation(Summary = "Login user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Login successfull", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Login failed.", Type = typeof(Status))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Status))]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            Status response = await _userAuth.LoginUserAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("Login", Name = "Login")]
        [SwaggerOperation(Summary = "Login user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Login successfull", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Login failed.", Type = typeof(Status))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Status))]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            Status response = await _userAuth.LoginUserAsync(request);
                return Ok(new
                {
                    Token = await _jwtGenToken.CreateToken(),
                    response,
                });
        }
    }
}
