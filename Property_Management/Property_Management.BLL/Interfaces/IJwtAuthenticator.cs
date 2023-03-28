using Property_Management.BLL.DTOs.Response;
using Property_Management.DAL.Entities;
using System.Security.Claims;

namespace Property_Management.BLL.Interfaces
{
    public interface IJWTAuthenticator
    {
        Task<JwtToken> GenerateJwtToken(ApplicationUser user, string expires = null, List<Claim> additionalClaims = null);
    }
}
