using Property_Management.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.BLL.DTOs.Request;


public class LoginRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}


public class TwoFactorLoginRequest
{
    public string UserId { get; set; }
    public string Token { get; set; }
}
public class ChangePasswordRequest
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string CurrentPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
}

public class AddUserToRoleRequest
{
    public string UserName { get; set; }

    [Required(ErrorMessage = "Role Name cannot be empty"), MinLength(2), MaxLength(50)]
    public string Role { get; set; }
}

public class VerifyAccountRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string NewPassword { get; set; }
    [Required]
    public string EmailConfirmationAuthenticationToken { get; set; }
    [Required]
    public string ResetPasswordAuthenticationToken { get; set; }

}

public class UserRegistrationRequest
{
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string ProfileImage { get; set; }

    [Phone]
    public string MobileNumber { get; set; }
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Firstname { get; set; }
    public UserRole Role { get; set; }

    [Required]
    public string LastName { get; set; }
    [Required]
    public string Occupation { get; set; }
    public string Address { get; set; }

    [Required]
    public UserType UserTypeId { get; set; }
}

public class ResetPasswordRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string AuthenticationToken { get; set; }
    [Required]
    public string NewPassword { get; set; }
}

public class UpdateRecoveryMailRequest
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Email { get; set; }
}

public class ChangeEmailRequest
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string NewEmail { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }
}

public class ChangeEmailRequestDto
{
    [Required]
    public string NewEmail { get; set; }

    [Required]
    public string RecoveryEmail { get; set; }
}