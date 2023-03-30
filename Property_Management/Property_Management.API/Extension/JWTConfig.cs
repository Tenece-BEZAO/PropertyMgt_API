using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Property_Management.BLL.DTOs.Responses;
using System.Text;


namespace Property_Management.API.Extension
{
    public static class JWTConfig
    {
        public static void ConfigureJWT(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var jwtSettings = services.Configure<JwtResponse>(builder.Configuration.GetSection("JwtConfig"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                string? key = builder.Configuration.GetSection("JwtConfig:Secret").Value;
                byte[] secretKey = Encoding.UTF8.GetBytes(key);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false, // set this true on production
                    ValidateAudience = false,
                    RequireExpirationTime = false, //need to updated when refreshed
                    ValidateLifetime = true,
                };
            });
        }

    }
}
