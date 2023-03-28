using Property_Management.DAL.Entities;

namespace Property_Management.BLL.DTOs.Response;

public class UserMailResponse
{
    public ApplicationUser User { get; set; }
    public string FirstName { get; set; }
}