﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Property_Management.DAL.Enums;
using Property_Management.DAL.Interfaces;
using System.Security.Claims;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Response;
using AutoMapper;
using Property_Management.BLL.Interfaces;
using MessageEncoder;
using Microsoft.AspNetCore.Authentication;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Implementations
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;


        public AuthenticationServices(IServiceFactory serviceFactory, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _mapper = _serviceFactory.GetService<IMapper>();
            _userManager = userManager;
        }

        public async Task<string> CreateUser(UserRegistrationRequest request)
        {
            ApplicationUser existingUser = await _userManager.FindByEmailAsync(request.Email);
            _userManager.FindByNameAsync("fhyueffe");
            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with Email {request.Email}");

            existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with username {request.UserName}");

            ApplicationUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email.ToLower(),
                UserName = request.UserName.Trim().ToLower(),
                UserTypeId = request.UserTypeId,
                PhoneNumber = request.MobileNumber,
                Active = true
            };

            //string password = AuthenticationExtension.GenerateRandomPassword();

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create user: {result.Errors.FirstOrDefault()?.Description}");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);


            AddUserToRoleRequest userRole = new() { UserName = user.UserName, Role = request.Role };

            await _serviceFactory.GetService<IRoleService>().AddUserToRole(userRole);

            UserMailRequest userMailDto = new()
            {
                User = user,
                FirstName = request.Firstname
            };

            // await _emailService.SendCreateUserEmail(userMailDto);
            return user.Id;
        }

        private async Task<JwtToken> GetTokenAsync(ApplicationUser user, string expires = null, List<Claim> additionalClaims = null)
        {
            JwtToken jwt = await _serviceFactory.GetService<IJWTAuthenticator>().GenerateJwtToken(user, expires, additionalClaims);
            return jwt;
        }
        private async Task<string> SaveChangedEmail(string userId, string decodedNewEmail, string decodedToken)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            await _userManager.ChangeEmailAsync(user, decodedNewEmail, decodedToken);
            await _userManager.UpdateNormalizedEmailAsync(user);
            await _unitOfWork.SaveChangesAsync();

            //Log.ForContext(new PropertyBagEnricher().Add("UpdatedEmail", new { userId, newEmail = decodedNewEmail }))
            //    .Information("Email Updated Successfully");

            return "Email changed successfully";
        }

        public async Task<string> ChangePassword(string userId, ChangePasswordRequest request)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid userId");
            }

            IdentityResult res = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (res.Succeeded)
            {
                return "Password changed successfully";
            }

            string errorMessage = string.Join("\n", res.Errors.Select(e => e.Description).ToList());

            throw new InvalidOperationException(errorMessage);
        }

        public async Task<string> ResetPassword(ResetPasswordRequest request)
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







        public async Task<AuthenticationResponse> UserLogin(LoginRequest request)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.UserName.ToLower().Trim());

            if (user == null)
                throw new InvalidOperationException("Invalid username or password");

            if (!user.Active)
                throw new InvalidOperationException("Account is not active");

            bool result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
                throw new InvalidOperationException("Invalid username or password");

            JwtToken userToken = await GetTokenAsync(user);

            List<Claim> userClaims = (await _userManager.GetClaimsAsync(user)).ToList();
            List<string> userRoles = (await _userManager.GetRolesAsync(user)).ToList();


            List<string> claims = userClaims.Select(x => x.Value).ToList();
            string? userType = user.UserTypeId.GetStringValue();
            bool? birthday = user.UserTypeId == UserType.Tenant && user.BirthDay.Date.DayOfYear == DateTime.Now.Date.DayOfYear;

            if (userType.ToLower() == "User")
            {
                //return new AuthenticationResponse { JwtToken = userToken, UserType = userType, FullName = fullName, Birthday = birthday, TwoFactor = false, UserId = user.Id };
            }

            /*await _emailService.SendTwoFactorAuthenticationEmail(user);

           Log.ForContext(new PropertyBagEnricher().Add("LoginResponse",
               new LoggedInUserResponse
               { FullName = fullName, UserType = userType, UserId = user.Id, TwoFactor = true },
             destructureObject: true)).Information("2FA Sent");
*/
            return new AuthenticationResponse { UserType = userType, UserName = user.UserName, UserId = user.Id, TwoFactor = true };
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

        public async Task UpdateRecoveryEmail(string userId, string email)
        {

            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found!");
            }

            //user.RecoveryMail = email;
            await _userManager.UpdateAsync(user);

            //Log.ForContext(new PropertyBagEnricher().Add("RecoveryEmail", email))
            //    .Information("Recovery Mail Updated Successfully");
        }

        public async Task ToggleUserActivation(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException($"The user with {nameof(userId)}: {userId} doesn't exist in the database.");
            }
            user.Active = !user.Active;

            await _userManager.UpdateAsync(user);

            //Log.ForContext(new PropertyBagEnricher().Add("ToggleState", user.Active)
            //).Information("User activation toggle successful");
        }


        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }

    //public async Task<ImpersonationLoginResponse> ImpersonationLogin(ImpersonationLoginRequest request)
    //{

    //    request.ImpersonatorId = Encoder.DecodeMessage(request.ImpersonatorId ?? throw new InvalidOperationException($"The {nameof(request.ImpersonatorId)} field is required !"));

    //    request.UserIdToImpersonate = Encoder.DecodeMessage(request.UserIdToImpersonate ?? throw new InvalidOperationException($"The {nameof(request.UserIdToImpersonate)} field is required !"));

    //    request.Token = Encoder.DecodeMessage(request.Token ?? throw new InvalidOperationException($"The {nameof(request.Token)} field is required !"));

    //    ApplicationUser impersonator = await _userManager.FindByIdAsync(request.ImpersonatorId);

    //    bool tokenIsValid = await _userManager.VerifyUserTokenAsync(impersonator ?? throw new UserNotFoundException(request.ImpersonatorId), "ImpersonationTokenProvider", "impersonation", request.Token);

    //    if (!tokenIsValid)
    //    {
    //        throw new InvalidOperationException("This link is invalid or has expired !");
    //    }


    //    if (string.Equals(request.UserIdToImpersonate, request.ImpersonatorId, StringComparison.CurrentCultureIgnoreCase))
    //    {
    //        throw new InvalidOperationException("You can't impersonate yourself !");
    //    }


    //    StaffProfileResponse userToImpersonateProfile = await _serviceFactory.GetService<IStaffService>().GetSingleStaff(request.UserIdToImpersonate);

    //    string roles = string.Join(',',
    //        await _unitOfWork.GetRepository<ApplicationRole>().GetQueryable(
    //                r => r.UserRoles.Any(ur => ur.UserId == request.UserIdToImpersonate.ToLower()),
    //                include: r => r.Include(x => x.UserRoles.Where(u => u.UserId == request.UserIdToImpersonate.ToLower())))
    //            .ToListAsync());

    //    string? passport = (await _serviceFactory.GetService<IUserDocumentService>().GetUserDocument(request.UserIdToImpersonate))?.Passport;

    //    return new ImpersonationLoginResponse
    //    {
    //        Email = userToImpersonateProfile.Email,
    //        Fullname = userToImpersonateProfile.FullName,
    //        Passport = passport,
    //        Roles = roles
    //    };

    //}


    //public async Task<UserResponse> GetUser(string userId)
    //{
    //    UserResponse user = new();
    //    StudentProfileResponse studentProfile = await _serviceFactory.GetService<IStudentService>().GetStudentProfile(userId);
    //    if (studentProfile != null)
    //    {
    //        user.Student = studentProfile;
    //    }

    //    LecturerResponse lecturer = await _serviceFactory.GetService<ILecturerService>().GetLecturerProfile(userId);
    //    if (lecturer != null)
    //    {
    //        user.Lecturer = lecturer;
    //    }

    //    StaffProfileResponse staff = await _serviceFactory.GetService<IStaffService>().GetSingleStaff(userId);
    //    if (staff != null)
    //    {
    //        user.Staff = staff;
    //    }

    //    return user;
    //}

}
