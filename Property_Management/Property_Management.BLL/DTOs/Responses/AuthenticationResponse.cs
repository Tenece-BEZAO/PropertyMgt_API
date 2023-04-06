namespace Property_Management.BLL.DTOs.Response;

public class AuthenticationResponse
{
    public string JwtToken { get; set; }
    public string UserType { get; set; }
    public string UserName { get; set; }
    public bool? Birthday { get; set; }
    public bool TwoFactor { get; set; }
}

public class JwtToken
{
    public string Token { get; set; }
    public DateTime Issued { get; set; }
    public DateTime? Expires { get; set; }
}