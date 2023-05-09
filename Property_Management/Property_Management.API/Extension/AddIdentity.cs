using Microsoft.AspNetCore.Identity;
using Property_Management.DAL.Context;
using Property_Management.DAL.Entities;

namespace Property_Management.API.Extension
{
    public static class AddIdentity
    {
        private const int _tenAttempts = 10;
        private const double _tenMinutes = _tenAttempts;
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<PMSDbContext>()
             .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/api/auth/Login-user");
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = _tenAttempts;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(_tenMinutes);
            });
        }
    }
}
