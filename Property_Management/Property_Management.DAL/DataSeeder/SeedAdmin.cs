using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Property_Management.DAL.Context;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;

namespace Property_Management.DAL.DataSeeder
{
    public class SeedAdmin
    {
        private const string _adminPassword = "AdminUser$%0@admin6<o>";
        private static readonly string _userId = Guid.NewGuid().ToString();
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            PMSDbContext context = serviceProvider.GetRequiredService<PMSDbContext>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();
            if (!await context.Users.AnyAsync())
            {
            var createUser = await userManager.CreateAsync(Admin(), _adminPassword);
            string? role = Admin().UserRole.GetStringValue();
            if (!createUser.Succeeded)
            {
                string errorMessage = createUser.Errors.FirstOrDefault().Description;
                Console.WriteLine($"Error occured while trying to create the user. {errorMessage}");
            }
            bool roleExist = await roleManager.RoleExistsAsync(role);
            if (roleExist)
                await userManager.AddToRoleAsync(Admin(), role);
            else
                await roleManager.CreateAsync(new IdentityRole(role));
            Console.WriteLine("User created successfully.");
            }
        }

        private static ApplicationUser Admin()
        {
            return new ApplicationUser()
            {
                Id = _userId,
                UserName = "Admin",
                ProfileImage = "https://lh3.googleusercontent.com/proxy/VWJS2-raqNHqMCSwPJOI0uFvouRZSQ2uWLdaxNsYVlxeyt8Q7rKCa9rB1o0Sk9vjjrW57QZ1Wk_5ac4rAbxWhU6lIg7LtUnT=s0-d",
                Email = "admin@domain.com",
                BirthDay = DateTime.UtcNow,
                PhoneNumber = "04934082832",
                Active = true,
                EmailConfirmed = true,
                UserTypeId = UserType.Admin,
                UserRole = UserRole.Admin
            };
        }
    }
}
