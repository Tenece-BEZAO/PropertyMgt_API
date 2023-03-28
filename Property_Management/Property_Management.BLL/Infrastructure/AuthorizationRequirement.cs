using Microsoft.AspNetCore.Authorization;

namespace Property_Management.BLL.Infrastructure
{
    public class AuthorizationRequirment : IAuthorizationRequirement
    {
        public int Success { get; set; }
    }
}
