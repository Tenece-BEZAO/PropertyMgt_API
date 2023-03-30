using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Property_Management.DAL.Context;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;

namespace Property_Management.DAL.SeedData
{
    public class Seed
    {
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            PMSDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<PMSDbContext>();

            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddRangeAsync(ListOfUsers());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<ApplicationUser> ListOfUsers()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "kelly",
                    ProfileImage = "https://lh3.googleusercontent.com/proxy/VWJS2-raqNHqMCSwPJOI0uFvouRZSQ2uWLdaxNsYVlxeyt8Q7rKCa9rB1o0Sk9vjjrW57QZ1Wk_5ac4rAbxWhU6lIg7LtUnT=s0-d",
                    Email = "k.amos@genesystechhub.com",
                    BirthDay = DateTime.UtcNow,
                    PhoneNumber = "09403820483",
                    Password = "*Admin@KellY#",
                    Active = true,
                    EmailConfirmed = true,
                    UserTypeId = UserType.Staff,
                    UserRole = UserRole.Admin
                },

                new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "gideonSmith",
                    ProfileImage = "https://th.bing.com/th/id/OIP.qt4e3xltJMIk22HHJc-yjwHaFL?pid=ImgDet&w=850&h=595&rs=1",
                    Email = "gideon@gmail.com",
                    BirthDay = DateTime.UtcNow,
                    PhoneNumber = "09334028430842",
                    Active = true,
                    EmailConfirmed =true,
                    UserTypeId = UserType.LandLord,
                    UserRole = UserRole.Admin,
                },

                new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "BizzMan",
                    ProfileImage = "https://i.pinimg.com/originals/f9/9d/50/f99d50383fea48614d78a0b586e2e00a.jpg",
                    Email = "bizz@gmail.com",
                    BirthDay = DateTime.UtcNow,
                    PhoneNumber = "09334028430842",
                    Active = true,
                    EmailConfirmed =true,
                    UserTypeId = UserType.Tenant,
                    UserRole = UserRole.User,
                }
            };

        }
    }
}

