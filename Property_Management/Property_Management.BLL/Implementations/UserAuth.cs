using MessageEncoder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Services;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;
using Property_Management.DAL.Interfaces;
using static Property_Management.BLL.Services.UserType;

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
        private readonly IHttpContextAccessor _contextAccessor;

        public UserAuth(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _landLordRepo = _unitOfWork.GetRepository<LandLord>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
            _staffRepo = _unitOfWork.GetRepository<Staff>();
            _userRepo = _unitOfWork.GetRepository<ApplicationUser>();
        }

        public async Task<AuthenticationResponse> CreateUserAsync(UserRegistrationRequest regRequest)
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
                EmailConfirmed = true,
                UserTypeId = regRequest.UserTypeId,
                UserRole = regRequest.Role,
            };

            IdentityResult createUser = await _userManager.CreateAsync(user, regRequest.Password);
            if (!createUser.Succeeded)
            {
               throw new InvalidOperationException($"User creation failed {createUser.Errors.FirstOrDefault()?.Description}");
            }
            string? userType = user.UserTypeId.GetStringValue().ToLower();

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
                      Console.WriteLine($"The user type {userType} was not found.");
                    break;
            }

            string token = GenJwtToken.CreateToken(user);
            string? role = user.UserRole.GetStringValue();
            bool? birthday = user.BirthDay.Date.DayOfYear == DateTime.Now.Date.DayOfYear;
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
                await _userManager.AddToRoleAsync(user, role);
            else
                await _roleManager.CreateAsync(new IdentityRole(role));

            return new AuthenticationResponse {UserName = user.UserName, UserType = userType, Birthday = birthday, TwoFactor = false, JwtToken = token, };
        }

        public async Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                throw new InvalidOperationException("User was not found.");

            bool IsPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!IsPassword)
                throw new InvalidOperationException("Invalid Credentials. Email of password does'nt exist.");

            if (!user.Active)
                throw new InvalidOperationException("Account is not active");

            string? userType = user.UserTypeId.GetStringValue();   
            bool? birthday = user.BirthDay.Date.DayOfYear == DateTime.Now.Date.DayOfYear;
           string userToken = GenJwtToken.CreateToken(user);
          return new AuthenticationResponse { JwtToken = userToken, UserType = userType, UserName = user.UserName, Birthday = birthday, TwoFactor = false};

            //await _emailService.SendTwoFactorAuthenticationEmail(user);

          /* Log.ForContext(new PropertyBagEnricher().Add("LoginResponse", 
               new LoggedInUserResponse { FullName = fullName, UserType = userType, UserId = user.Id, TwoFactor = true },  destructureObject: true)).Information("2FA Sent");*/

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
            ApplicationUser user = await _userManager.FindByIdAsync(changePasswordRequest.UserId);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid userId");
            }

            IdentityResult res = await _userManager.ChangePasswordAsync(user, changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword);
            if (!res.Succeeded)
                throw new InvalidOperationException("Sorry!, error occured while trying to update user password. Try again!");
            return new Response
            {
                StatusCode = 200,
                Message = "Password changed successfully",
                Action = "Change of password"
            };
            //string errorMessage = string.Join("\n", res.Errors.Select(e => e.Description).ToList());
           // throw new InvalidOperationException(errorMessage);
        }

        public async Task<Response> ResetPasswordAsync(ResetPasswordRequest request)
        {
            //string decodedEmail = Encoder.DecodeMessage(request.Email);
            string decodedToken = Encoder.DecodeMessage(request.AuthenticationToken);
            ApplicationUser user = await _userManager.FindByNameAsync(request.UserName);

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
                //Log.Information(msg);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Password Reset successfully.",
                    Action = "Password resetion."
                };
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

        public async Task<Response> ChangeEmail(ChangeEmailRequest request)
        {

            //string decodedEmail = Encoder.DecodeMessage(request.Email);
          //  string decodedNewEmail = Encoder.DecodeMessage(request.NewEmail);
            //string decodedToken = Encoder.DecodeMessage(request.Token);
            string? userId = _contextAccessor.HttpContext?.User.ToString();
            ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
            throw new InvalidOperationException("Recovery email not found.");
            }
          IdentityResult oops =  await _userManager.ChangeEmailAsync(user, request.NewEmail, request.Token);
            string message = oops.Errors.FirstOrDefault().Description;
            if (!oops.Succeeded)
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
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

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
            //Log.ForContext(new PropertyBagEnricher().Add("ToggleState", user.Active)
            //).Information("User activation toggle successful");
        }

    }
}
