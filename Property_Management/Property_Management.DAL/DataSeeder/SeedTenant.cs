using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Property_Management.DAL.Context;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;

namespace Property_Management.DAL.DataSeeder
{
    public class SeedTenant
    {
        private const string _tenantPassword = "tenantUser$%0@admin6<o>";
        private static readonly string _userId = Guid.NewGuid().ToString();
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            PMSDbContext context = serviceProvider.GetRequiredService<PMSDbContext>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();

            if (!await context.Tenants.AnyAsync())
            {
                var createUser = await userManager.CreateAsync(User(), _tenantPassword);
            if (!createUser.Succeeded)
            {
                string errorMessage = createUser.Errors.FirstOrDefault().Description;
                Console.WriteLine($"Error occured while trying to create the user. {errorMessage}");
            }

                string? role = User().UserRole.GetStringValue();
                bool roleExist = await roleManager.RoleExistsAsync(role);
                if (roleExist)
                    await userManager.AddToRoleAsync(User(), role);
                else
                    await roleManager.CreateAsync(new IdentityRole(role));

                await context.Tenants.AddAsync(Tenant());
            int rowChanges = await context.SaveChangesAsync();
            if (rowChanges <= 0)
            {
               Console.WriteLine("Creating tenant failed.");
            }

               Console.WriteLine("Tenant created successfully.");
            }
        }

        private static ApplicationUser User()
        {
            return new ApplicationUser()
            {
                Id = _userId,
                UserName = "tenant",
                ProfileImage = "https://lh3.googleusercontent.com/proxy/VWJS2-raqNHqMCSwPJOI0uFvouRZSQ2uWLdaxNsYVlxeyt8Q7rKCa9rB1o0Sk9vjjrW57QZ1Wk_5ac4rAbxWhU6lIg7LtUnT=s0-d",
                Email = "user@tenant.com",
                NormalizedEmail = "user@tenant.com",
                BirthDay = DateTime.UtcNow,
                PhoneNumber = "09403820483",
                Active = true,
                EmailConfirmed = true,
                UserTypeId = UserType.Tenant,
                UserRole = UserRole.Tenant
            };
        }
        private static Tenant Tenant()
        {
            return new Tenant
            {
                TenantId = Guid.NewGuid().ToString(),
                UserId = _userId,
                FirstName = "tenant",
                LastName = "tenant",
                Email = "user@tenant.com",
                PhoneNumber = "0933480842",
                Occupation = "tenant.",
                Address = "property@tenant.",
            };
        }
    }
}
