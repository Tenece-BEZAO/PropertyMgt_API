namespace Property_Management.BLL.DTOs.Response;

public class AuthenticationResponse
{
    public JwtToken JwtToken { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public bool? Birthday { get; set; }
    public bool EmailSent { get; set; }
    public bool TwoFactorAuth { get; set; }
}

public class JwtToken
{
    public string Token { get; set; }
    public DateTime Issued { get; set; }
    public DateTime? Expires { get; set; }
}

