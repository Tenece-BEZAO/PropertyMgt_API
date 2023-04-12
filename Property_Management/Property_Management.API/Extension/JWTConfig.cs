using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Property_Management.API.Extension
{
    public static class JWTConfig
    {
        public static void ConfigureJWT(this IServiceCollection services, WebApplicationBuilder builder)
        {
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
                string? issure = builder.Configuration.GetSection("JwtConfig:ValidIssure").Value;
                string? audience = builder.Configuration.GetSection("JwtConfig:ValidAudience").Value;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false, // set this true on production
                    ValidateAudience = false,
                    RequireExpirationTime = true, //need to updated when refreshed
                    ValidateLifetime = true,
                    ValidIssuer = issure,
                    ValidAudience = audience,
                };
            });
        }

    }
}
