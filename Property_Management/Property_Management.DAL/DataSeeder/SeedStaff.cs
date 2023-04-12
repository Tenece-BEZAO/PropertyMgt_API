using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Property_Management.DAL.Context;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;
using System.Data;

namespace Property_Management.DAL.DataSeeder
{
    public class SeedStaff
    {

        private const string _staffPassword = "StaffUser$%0@admin6<o>";
        private static readonly string _userId = Guid.NewGuid().ToString();
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            PMSDbContext context = serviceProvider.GetRequiredService<PMSDbContext>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();

            if (!await context.Employees.AnyAsync())
            {
                var createUser = await userManager.CreateAsync(User(), _staffPassword);
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

                await context.Employees.AddAsync(Staff());
                int rowChanges = await context.SaveChangesAsync();
                if (rowChanges <= 0)
                {
                    Console.WriteLine("Creating tenant failed.");
                }

                Console.WriteLine("Staff created successfully.");
            }
        }

        private static ApplicationUser User()
        {
            return new ApplicationUser()
            {
                Id = _userId,
                UserName = "staff",
                ProfileImage = "https://lh3.googleusercontent.com/proxy/VWJS2-raqNHqMCSwPJOI0uFvouRZSQ2uWLdaxNsYVlxeyt8Q7rKCa9rB1o0Sk9vjjrW57QZ1Wk_5ac4rAbxWhU6lIg7LtUnT=s0-d",
                Email = "user@staff.com",
                BirthDay = DateTime.UtcNow,
                PhoneNumber = "09403820483",
                Active = true,
                EmailConfirmed = true,
                UserTypeId = UserType.Staff,
                UserRole = UserRole.User
            };
        }
        private static Staff Staff()
        {
            return new Staff
            {
                StaffId = Guid.NewGuid().ToString(),
                UserId = _userId,
                FirstName = "staff",
                LastName = "staff",
                Email = "user@staff.com",
                PhoneNumber = "09274972",
                Occupation = "staff",
                Address = "property@staff"
            };
        }
    }
}

