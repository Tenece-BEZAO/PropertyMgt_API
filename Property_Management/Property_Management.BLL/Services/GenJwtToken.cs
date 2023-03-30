using Microsoft.IdentityModel.Tokens;
using Property_Management.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Property_Management.BLL.Services
{
    public class GenJwtToken
    {
        public static string CreateToken(ApplicationUser user)
        {
            JwtSecurityTokenHandler jwTokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes("K3llY0dka4938-4380ls99430-943ofkakjslxzdoyb");

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject= new ClaimsIdentity(new []
                {
                    new Claim("UserId", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            SecurityToken token = jwTokenHandler.CreateToken(tokenDescriptor);
            return jwTokenHandler.WriteToken(token);
        }

    }
}
