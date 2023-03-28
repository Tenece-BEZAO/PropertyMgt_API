namespace Property_Management.BLL.DTOs.Response;

public class MenuClaimsResponse
{
    public string Menu { get; set; }
    public IEnumerable<string> Claims { get; set; }
}