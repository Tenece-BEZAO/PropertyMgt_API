using MessageEncoder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Utilities;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;
using Property_Management.DAL.Interfaces;
using System.Security.Claims;
using static Property_Management.BLL.Utilities.UserType;

namespace Property_Management.BLL.Implementations
{
    public class UserAuth : IUserAuth
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<LandLord> _landLordRepo;
        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IRepository<Staff> _staffRepo;
        private readonly IRepository<ApplicationUser> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendMailService _sendMailService;
        private readonly ISMSService _sendSMS;
        private readonly IConfiguration _configuration;

        public UserAuth(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork,
            ISendMailService sendMailService, ISMSService sendSMS, IConfiguration configuration)
        {
            _sendMailService = sendMailService;
            _sendSMS = sendSMS;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _landLordRepo = _unitOfWork.GetRepository<LandLord>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
            _staffRepo = _unitOfWork.GetRepository<Staff>();
            _userRepo = _unitOfWork.GetRepository<ApplicationUser>();
        }

        public async Task<EmailResponse> CreateUserAsync(UserRegistrationRequest regRequest)
        {
            ApplicationUser? userExist = await _userManager.FindByEmailAsync(regRequest.Email);
            if (userExist != null)
                throw new InvalidOperationException("User already exists");

            bool phoneExist = await _userRepo.AnyAsync(user => user.PhoneNumber == regRequest.MobileNumber);
            if (phoneExist)
                throw new InvalidOperationException("Phone number already taken.");

            string userId = Guid.NewGuid().ToString();
            string tenantId = Guid.NewGuid().ToString();
            string staffId = Guid.NewGuid().ToString();
            string? userType = regRequest.UserTypeId.GetStringValue().ToLower();
            string? userRole = regRequest.Role.GetStringValue().ToLower();

            if (userRole == null) throw new InvalidOperationException("User role provided does not exist.");
            if (userType == null) throw new InvalidOperationException("User type provided does not exist.");


            ApplicationUser user = new()
            {
                Id = userId,
                UserName = regRequest.UserName.Trim().ToLower(),
                NormalizedEmail = regRequest.Email.Trim().ToUpper(),
                ProfileImage = regRequest.ProfileImage,
                Email = regRequest.Email.Trim().ToLower(),
                BirthDay = DateTime.UtcNow,
                PhoneNumber = regRequest.MobileNumber,
                Active = true,
                UserTypeId = regRequest.UserTypeId,
                UserRole = regRequest.Role,
            };

            IdentityResult createUser = await _userManager.CreateAsync(user, regRequest.Password);
            if (!createUser.Succeeded)
            {
                throw new InvalidOperationException($"User creation failed {createUser.Errors.FirstOrDefault()?.Description}");
            }

            switch (userType)
            {
                case "landlord":
                    await _landLordRepo.AddAsync(NewLandLord(regRequest, userId));
                    break;
                case "tenant":
                    await _tenantRepo.AddAsync(NewTenant(regRequest, tenantId, userId));
                    break;
                case "staff":
                case "manager":
                    await _staffRepo.AddAsync(NewStaff(regRequest, staffId, userId));
                    break;
                default:
                    Console.WriteLine($"This user will be created as just a {userType} for now.");
                    break;
            }

            string? role = user.UserRole.GetStringValue();
            bool? birthday = user.BirthDay.Date.DayOfYear == DateTime.Now.Date.DayOfYear;
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
                await _userManager.AddToRoleAsync(user, role);
            else
                await _roleManager.CreateAsync(new IdentityRole(role));

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string decodedToken = Encoder.EncodeMessage(token);

            var confirmEmailReponse = new ConfirmEmailResponse { UserId = user.Id, Email = user.Email, Token = decodedToken, UserName = user.UserName };
            return (await _sendMailService.UserCreatedMailAsync(confirmEmailReponse));
        }

        public async Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                throw new InvalidOperationException("User was not found.");

            bool IsPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!IsPassword)
                throw new InvalidOperationException("Invalid Credentials. Email of password does'nt exist.");

            if (!user.Active)
                throw new InvalidOperationException("Account is not active");
            if (!user.EmailConfirmed)
                throw new InvalidOperationException($"HI {user.UserName}! please verify your email address first. Check your email we have sent you a verification code to confirm your email.");

            if (user.TwoFactorEnabled)
            {
                string TwoFactorToken = _userManager.Options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultEmailProvider;
                string? authToken = await _userManager.GenerateTwoFactorTokenAsync(user, TwoFactorToken);
                string? message = $"Hey {user.NormalizedUserName}! we have sent you a verification link to {user.Email} and also to your phone number {user.PhoneNumber}.";
                ConfirmEmailResponse confirmEmailResponse = new ConfirmEmailResponse { Token = authToken, UserName = user.UserName, TwoFactorAuth = true, Email = user.Email };
                (bool, string) smsMessage = await _sendSMS.SendSmsAsync(user.PhoneNumber, message);
                Console.WriteLine(smsMessage);
                await _sendMailService.SendTwoFactorAuthenticationEmailAsync(confirmEmailResponse);
                return new AuthenticationResponse { UserId = user.Id, UserName = message, TwoFactorAuth = true, };
            }



            bool? birthday = user.BirthDay.Date.DayOfYear == DateTime.Now.Date.DayOfYear;
            string userToken = GenJwtToken.CreateToken(user);
            DateTime TwoWeeks = DateTime.Now.AddDays(14);
            return new AuthenticationResponse
            {
                UserId = user.Id,
                JwtToken = new JwtToken { Token = userToken, Expires = TwoWeeks, Issued = DateTime.UtcNow },
                UserName = user.UserName,
                Birthday = birthday,
                TwoFactorAuth = false,
            };
        }

        public async Task<Response> Toggle2FAAuthAsync(string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException($"User with the id {userId} was not found.");

            user.TwoFactorEnabled = !user.TwoFactorEnabled;
            await _userRepo.UpdateAsync(user);
            return new Response
            {
                Action = "Two Factor Auth",
                Message = $"Two factor authentication was updated for {user.UserName}",
                StatusCode = 200,
                IsEmailSent = false
            };
        }

        public async Task<AuthenticationResponse> TwoFactorLoginAsync(TwoFactorLoginRequest request)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                throw new InvalidOperationException("Invalid user");

            string tokenType = _userManager.Options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultEmailProvider;
            bool result = await _userManager.VerifyTwoFactorTokenAsync(user, tokenType, request.Token);

            if (!result)
                throw new InvalidOperationException("Invalid token");

            string userToken = GenJwtToken.CreateToken(user);
            bool? birthday = (user.BirthDay.Date == DateTime.Now) ? true : false;
            return new AuthenticationResponse
            {
                JwtToken = new JwtToken { Token = userToken, Expires = DateTime.UtcNow.AddDays(14), Issued = DateTime.UtcNow },
                UserName = user.UserName,
                Birthday = birthday,
                TwoFactorAuth = true
            };
        }

        public async Task<Response> ConfirmEmailAsync(string id, string token)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new InvalidOperationException("User with this id was not found.");

            string decodedToken = Encoder.DecodeMessage(token);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
                throw new InvalidOperationException("Email confirmation was not successfull.");

            return new Response { Action = "Email Confirmation", Message = "Email confirmation successfull.", IsEmailSent = false, StatusCode = 200 };
        }

        public async Task<ProfileResponse> GetTenantProfileDetails(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new InvalidOperationException("This user was not found.");
            Tenant tenant = await _tenantRepo.GetSingleByAsync(
                predicate: t => t.UserId == user.Id,
                include: t => t.Include(u => u.Lease).ThenInclude(u => u.Property));

            if (tenant == null)
                throw new InvalidOperationException("This use is not a tenant.");


            return new ProfileResponse
            {
                UserEmail = tenant.Email,
                UserName = user.UserName,
                ProfileImage = user.ProfileImage,
                PropertyName = tenant.Property.Name,
                //LeaseDescription = tenant.Lease.FirstOrDefault()?.Description
            };
        }

        public async Task<Response> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return new Response
            {
                StatusCode = 200,
                Action = "User signout",
                Message = "You have logged out successfully."
            };

        }

        public async Task<Response> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(changePasswordRequest.UserId);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid userId");
            }

            IdentityResult res = await _userManager.ChangePasswordAsync(user, changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword);
            if (!res.Succeeded)
            {
                string errorMessage = string.Join("\n", res.Errors.Select(e => e.Description).ToList());
                throw new InvalidOperationException($"Sorry!, error occured while trying to update user password {errorMessage}. Try again!");
            }
            return new Response
            {
                StatusCode = 200,
                Message = "Password changed successfully",
                Action = "Change of password"
            };
        }

        public async Task<Response> ForgortPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new InvalidOperationException("User with this email provided was not found.");

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string encodedToken = Encoder.EncodeMessage(token);
            string link = $"{_configuration["BaseURL"]}/api/auth/recet-password?email={email}&token={encodedToken}";
            EmailResponse emailResponse = await _sendMailService.SendEmailPasswordResetMailAsync(user, link);

            return new Response
            {
                StatusCode = 200,
                Message = "Check your email we have sent you a link to recet your password.",
                Action = "Forgort password",
                IsEmailSent = emailResponse.Sent

            };
        }

        public async Task<Response> ResetPasswordAsync(ResetPasswordRequest request)
        {
            string decodedToken = Encoder.DecodeMessage(request.AuthenticationToken);
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid email");
            }

            bool isTokenValid = !await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", decodedToken);
            if (isTokenValid)
            {
                throw new InvalidOperationException("Invalid Authentication Token");
            }

            IdentityResult res = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if (res.Succeeded)
            {
                string message = "Password reset successfully";
                EmailResponse emailReponse = await _sendMailService.RecetPasswordSuccessMailAsync(user, message);
                return new Response
                {
                    Message = message,
                    Action = "Password Reset",
                    IsEmailSent = emailReponse.Sent,
                    StatusCode = 200,
                };
            }

            string errorMessage = string.Join("\n", res.Errors.Select(e => e.Description).ToList());
            throw new InvalidOperationException(errorMessage);
        }

        public async Task<Response> ChangeEmailRequestAsync(string userId, string newEmail)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User was not found.");
            string? token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            string EncodedToken = Encoder.EncodeMessage(token);
            return new Response
            {
                Token = EncodedToken,
                Message = "Token to change email was generated successfully.",
                Action = "Change email request",
                StatusCode = 200,
            };
        }

        public async Task<Response> ChangeEmail(ChangeEmailRequest request)
        {
            string decodedToken = Encoder.DecodeMessage(request.Token);
            ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new InvalidOperationException("Recovery email not found.");
            }
            IdentityResult response = await _userManager.ChangeEmailAsync(user, request.NewEmail, decodedToken);
            string? message = response.Errors.FirstOrDefault()?.Description;
            if (!response.Succeeded)
                throw new InvalidOperationException($"Sorry, the update failed. {message}");

            await _userManager.UpdateNormalizedEmailAsync(user);
            return new Response
            {
                StatusCode = 200,
                Message = "Email changed succefully.",
                Action = "Change of email."
            };
        }

        public async Task<Response> ToggleUserActivation(string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException($"The user with {nameof(userId)}: {userId} doesn't exist in the database.");
            }
            user.Active = !user.Active;

            await _userManager.UpdateAsync(user);
            return new Response
            {
                StatusCode = 200,
                Message = "User activation toggle successful",
                Action = "User activation."
            };
        }

        public async Task<Response> GoogleLoginAsync()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                // return RedirectToAction(nameof(Login));
                return new Response { StatusCode = 400, };

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
            {
                return new Response { Action = "Google Login", Message = "User already exist and is signed-in Successfully.", StatusCode = 200 };
            }
            ApplicationUser user = new ApplicationUser
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            IdentityResult identResult = await _userManager.CreateAsync(user);

            if (!identResult.Succeeded)
                throw new InvalidOperationException("Sorry! user creation failed. {errMessage}");

                identResult = await _userManager.AddLoginAsync(user, info);
                if (identResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return new Response { Action = "Google Login", Message = "User signed-in Successfully.", StatusCode = 200 };
                }
            string? errorMessage = identResult.Errors.Select(e => e.Description).ToString();
            return new Response { Message = errorMessage, StatusCode = 500 };
        }
        public async Task<Response> FaceBookLoginAsync()
        {
            throw new NotImplementedException();
        }
    }
}
