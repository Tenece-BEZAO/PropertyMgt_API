using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Property_Management.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Property_Management.BLL.Services
{
    public class JwtGenToken
    {
        public async Task<string> CreateToken(ApplicationUser user, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user, userManager);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims,configuration);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            
            var key = Encoding.ASCII.GetBytes("K3llY0dka4938-4380ls99430-943ofkakjslxzdoyb");
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims(ApplicationUser appUser, UserManager<ApplicationUser> userManager)
        {
            var claims = new List<Claim> {new Claim(ClaimTypes.Name, appUser.UserName)};
            var roles = await userManager.GetRolesAsync(appUser);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])), 
            signingCredentials: signingCredentials);
            return tokenOptions;
        }

    }
}
