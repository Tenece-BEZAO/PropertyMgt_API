namespace Property_Management.BLL.DTOs.Response;

public class AuthenticationResponse
{
    public string JwtToken { get; set; }
    public string UserType { get; set; }
    public string UserName { get; set; }
    public IEnumerable<string> MenuItems { get; set; }
    public bool? Birthday { get; set; }
    public bool TwoFactor { get; set; }
    public string UserId { get; set; }
    public string ImpersonatorUsername { get; set; }
    public bool IsImpersonating { get; set; }

    public static implicit operator AuthenticationResponse(string v)
    {
        throw new NotImplementedException();
    }
}

public class JwtToken
{
    public string Token { get; set; }
    public DateTime Issued { get; set; }
    public DateTime? Expires { get; set; }
}