using Microsoft.AspNetCore.Identity;
using Property_Management.BLL.DTOs;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Services;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Property_Management.BLL.Implementations
{
    public class UserAuth : IUserAuth
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Status _status = new();

        public UserAuth(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

       
        public async Task<Status> CreateUserAsync(UserRegistrationRequest regRequest)
        {
            ApplicationUser? userExist = await _userManager.FindByEmailAsync(regRequest.Email);
            if (userExist != null)
            {
                _status.StatusCode = 0;
                _status.Message = "User already exists";
                return _status;
            }

            ApplicationUser User = new()
            {
                Id = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = regRequest.Firstname,
                UserName = regRequest.UserName,
                Email = regRequest.Email,
                NormalizedEmail = regRequest.Email,
                BirthDay = DateTime.UtcNow,
                PhoneNumber = regRequest.MobileNumber,
                Password = regRequest.Password,
                Occupation = regRequest.Occupation,
                Active = true,
                EmailConfirmed = true
            };

            IdentityResult createUser = await _userManager.CreateAsync(User, regRequest.Password);
            if (!createUser.Succeeded)
            {
                _status.StatusCode = 0;
                _status.Message = $"User creation failed {createUser.Errors.FirstOrDefault()?.Description}";
                return _status;
            }

            //role Manager
            string? role = regRequest.UserTypeId.GetStringValue();
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
                await _userManager.AddToRoleAsync(User, role);
            else
                await _roleManager.CreateAsync(new IdentityRole(role));
            _status.StatusCode = 1;
            _status.Message = "User has been Registered successfully.";
            return _status;
        }

        public async Task<Status> LoginUserAsync(LoginRequest loginRequest)
        {
            ApplicationUser User = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (User == null)
                throw new InvalidOperationException("User was not found.");

            bool IsPassword = await _userManager.CheckPasswordAsync(User, loginRequest.Password);
            if (!IsPassword)
                throw new InvalidOperationException("Invalid Credentials. Email of password does'nt exist.");

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(User, loginRequest.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(User);
                var authClaims = new List<Claim>();
                authClaims.Add(new Claim(ClaimTypes.Name, User.UserName));

                string? userRole = userRoles.FirstOrDefault();
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                _status.StatusCode = 1;
                _status.Message = "Logged in Succesffully.";
                return _status;
            }
            if (signInResult.IsLockedOut)
                throw new InvalidOperationException("User locked out.");

            throw new InvalidOperationException("Sorry error occured. Please try again.");
        }

        public async Task<Status> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _status.StatusCode = 1;
            _status.Message = "You have logged out successfully.";
            return _status;
        }
    }
}
