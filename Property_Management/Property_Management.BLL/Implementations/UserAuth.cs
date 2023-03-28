using MessageEncoder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Services;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;
using Property_Management.DAL.Interfaces;
using System.Security.Claims;

namespace Property_Management.BLL.Implementations
{
    public class UserAuth : IUserAuth
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<ApplicationUser> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly SuccessResponse _reponse = new();
        private readonly JwtGenToken _jwtGenToken = new();

        public UserAuth(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _contextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepo = _unitOfWork.GetRepository<ApplicationUser>();
        }

        public async Task<SuccessResponse> CreateUserAsync(UserRegistrationRequest regRequest)
        {
            ApplicationUser? userExist = await _userManager.FindByEmailAsync(regRequest.Email);
            if (userExist != null)
            {

                _reponse.StatusCode = 400;
                _reponse.Message = "User already exists";
                return _reponse;
            }

            ApplicationUser User = new()
            {
                Id = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = regRequest.Firstname,
                UserName = regRequest.UserName,
                NormalizedUserName = regRequest.UserName.ToUpper(),
                Email = regRequest.Email,
                NormalizedEmail = regRequest.Email.ToUpper(),
                BirthDay = DateTime.UtcNow,
                PhoneNumber = regRequest.MobileNumber,
                Password = regRequest.Password,
                Occupation = regRequest.Occupation,
                Active = true,
                EmailConfirmed = true,
                UserTypeId = regRequest.UserTypeId,
                UserRole = UserRole.User,
            };

            IdentityResult createUser = await _userManager.CreateAsync(User, regRequest.Password);
            if (!createUser.Succeeded)
            {
                _reponse.StatusCode = 400;
                _reponse.Action = "User registration";
                _reponse.Message = $"User creation failed {createUser.Errors.FirstOrDefault()?.Description}";
                return _reponse;
            }

            //role Manager
            string? role = User.UserRole.GetStringValue();
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
                await _userManager.AddToRoleAsync(User, role);
            else
                await _roleManager.CreateAsync(new IdentityRole(role));

            _reponse.StatusCode = 201;
            _reponse.Action = "User registration";
            _reponse.Message = "User has been Registered successfully.";
            return _reponse;
        }

        public async Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
                throw new InvalidOperationException("User was not found.");
            if (!user.Active)
                throw new InvalidOperationException("Account is not active");

            bool IsPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!IsPassword)
                throw new InvalidOperationException("Invalid Credentials. Email of password does'nt exist.");

                var userRoles = await _userManager.GetRolesAsync(user);
            string? userType = user.UserTypeId.GetStringValue();   
            bool? birthday = user.BirthDay.Date.DayOfYear == DateTime.Now.Date.DayOfYear;
            string fullName = $"{user.LastName} {user.FirstName}";
           string userToken = await _jwtGenToken.CreateToken(user, _userManager, _configuration);

            var authClaims = new List<Claim>();
            authClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            string userRole = userRoles.FirstOrDefault();
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            if (userType?.ToLower() == "tenant")
            {
                return new AuthenticationResponse { JwtToken = userToken, UserType = userType, FullName = fullName, Birthday = birthday, TwoFactor = false, UserId = user.Id };
            }
            return new AuthenticationResponse { UserType = userType, FullName = fullName, UserId = user.Id, TwoFactor = true };

            //await _emailService.SendTwoFactorAuthenticationEmail(user);

          /* Log.ForContext(new PropertyBagEnricher().Add("LoginResponse", 
               new LoggedInUserResponse { FullName = fullName, UserType = userType, UserId = user.Id, TwoFactor = true },  destructureObject: true)).Information("2FA Sent");*/

        }

        public async Task<SuccessResponse> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _reponse.StatusCode = 1;
            _reponse.Action = "User signout";
            _reponse.Message = "You have logged out successfully.";
            return _reponse;
        }

        private async Task<string> SaveChangedEmail(string userId, string decodedNewEmail, string decodedToken)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            await _userManager.ChangeEmailAsync(user, decodedNewEmail, decodedToken);
            await _userManager.UpdateNormalizedEmailAsync(user);
            await _userRepo.SaveAsync();

            //Log.ForContext(new PropertyBagEnricher().Add("UpdatedEmail", new { userId, newEmail = decodedNewEmail }))
            //    .Information("Email Updated Successfully");

            return "Email changed successfully";
        }

        public async Task<string> ChangePassword(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid userId");
            }

            //IdentityResult res = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            user.Password = "K2ll5%20&b@buffering#";
         await _userRepo.UpdateAsync(user);
            return "Password changed successfully";
            //string errorMessage = string.Join("\n", res.Errors.Select(e => e.Description).ToList());
           // throw new InvalidOperationException(errorMessage);
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest request)
        {
            string decodedEmail = Encoder.DecodeMessage(request.Email);
            string decodedToken = Encoder.DecodeMessage(request.AuthenticationToken);
            ApplicationUser user = await _userManager.FindByEmailAsync(decodedEmail);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid email");
            }

            if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", decodedToken))
            {
                throw new InvalidOperationException("Invalid Authentication Token");
            }

            IdentityResult res = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if (res.Succeeded)
            {
                string msg = "Password Reset successfully";

                //Log.Information(msg);

                return msg;
            }

            string errorMessage = string.Join("\n", res.Errors.Select(e => e.Description).ToList());

            throw new InvalidOperationException(errorMessage);
        }

        public async Task<string> VerifyUser(VerifyAccountRequest request)
        {
            string username = Encoder.DecodeMessage(request.Username);
            string emailConfirmationToken = Encoder.DecodeMessage(request.EmailConfirmationAuthenticationToken);
            string resetPasswordToken = Encoder.DecodeMessage(request.ResetPasswordAuthenticationToken);

            ApplicationUser user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid username");
            }

            if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.EmailConfirmationTokenProvider, "EmailConfirmation", emailConfirmationToken))
            {
                throw new InvalidOperationException("Invalid Authentication Token");
            }

            if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPasswordToken))
            {
                throw new InvalidOperationException("Invalid Authentication Token");
            }

            IdentityResult emailRes = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
            IdentityResult passwordRes = await _userManager.ResetPasswordAsync(user, resetPasswordToken, request.NewPassword);


            if (emailRes.Succeeded && passwordRes.Succeeded)
            {
                user.Active = true;
                await _userManager.UpdateAsync(user);

                return "Password reset successfully";
            }

            string errorMessage = string.Join("\n", emailRes.Errors.Select(e => e.Description).ToList()) +
                                  string.Join("\n", passwordRes.Errors.Select(e => e.Description).ToList());

            throw new InvalidOperationException(errorMessage);
        }

        public async Task<string> ChangeEmail(ChangeEmailRequest request)
        {

            string decodedEmail = Encoder.DecodeMessage(request.Email);
            string decodedNewEmail = Encoder.DecodeMessage(request.NewEmail);
            string decodedToken = Encoder.DecodeMessage(request.Token);
            string? _userId = _contextAccessor.HttpContext?.User.ToString();

            if (_userId != null)
                return await SaveChangedEmail(_userId, decodedNewEmail, decodedToken);

            throw new InvalidOperationException("Recovery email not found.");
        }

        public async Task<string> ToggleUserActivation(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException($"The user with {nameof(userId)}: {userId} doesn't exist in the database.");
            }
            user.Active = !user.Active;

            await _userManager.UpdateAsync(user);
            return _reponse.Message = "User activation toggle successful";
            //Log.ForContext(new PropertyBagEnricher().Add("ToggleState", user.Active)
            //).Information("User activation toggle successful");
        }

    }
}
