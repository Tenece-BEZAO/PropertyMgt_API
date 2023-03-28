using Microsoft.AspNetCore.Identity;
using Property_Management.DAL.Context;
using Property_Management.DAL.Entities;

namespace Property_Management.API.Extension
{
    public static class AddIdentity
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<PMSDbContext>()
             .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/api/Auth/Login-user");
        }
    }
}
