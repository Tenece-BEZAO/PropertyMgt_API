using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Property_Management.DAL.Context;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;

namespace Property_Management.DAL.DataSeeder
{
    public class SeedLandLord
    {
        private const string _landLordPassword = "LandLordUser$%0@admin6<o>";
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
                var createUser = await userManager.CreateAsync(User(), _landLordPassword);
            string? role = User().UserRole.GetStringValue();
            if (!createUser.Succeeded)
            {
                string errorMessage = createUser.Errors.FirstOrDefault().Description;
                Console.WriteLine($"Error occured while trying to create the user. {errorMessage}");
            }
            bool roleExist = await roleManager.RoleExistsAsync(role);
            if (roleExist)
                await userManager.AddToRoleAsync(User(), role);
            else
                await roleManager.CreateAsync(new IdentityRole(role));

            await context.LordLords.AddAsync(Landlord());
            int rowChanges = await context.SaveChangesAsync();
            if (rowChanges <= 0)
            {
                    Console.WriteLine("Landlord failed to create.");
            }

            Console.WriteLine("Landlord created successfully.");
            }
        }

        private static ApplicationUser User()
        {
            return new ApplicationUser()
            {
                Id = _userId,
                UserName = "landlord",
                ProfileImage = "https://lh3.googleusercontent.com/proxy/VWJS2-raqNHqMCSwPJOI0uFvouRZSQ2uWLdaxNsYVlxeyt8Q7rKCa9rB1o0Sk9vjjrW57QZ1Wk_5ac4rAbxWhU6lIg7LtUnT=s0-d",
                Email = "user@landlord.com",
                BirthDay = DateTime.UtcNow,
                PhoneNumber = "09403820483",
                Active = true,
                EmailConfirmed = true,
                UserTypeId = UserType.LandLord,
                UserRole = UserRole.LandLord,
            };
        }
        private static LandLord Landlord()
        {
            return new LandLord
            {
                Id = Guid.NewGuid().ToString(),
                UserId = _userId,
                FirstName = "landlord",
                LastName = "propertyOwner",
                Email = "user@landlord.com",
                PhoneNumber = "09334028430842",
                Occupation = "landlord.",
                Address = "propery@landlord.",
            };
        }
    }
}
