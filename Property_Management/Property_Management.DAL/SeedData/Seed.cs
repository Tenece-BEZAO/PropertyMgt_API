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
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Kellyn",
                    UserName = "Kelly",
                    NormalizedUserName = "KELLY",
                    Email = "k.amos@genesystechhub.com",
                    NormalizedEmail = "K.AMOS@GENSYSTECHUB.COM",
                    BirthDay = DateTime.UtcNow,
                    PhoneNumber = "09403820483",
                    Password = "*Admin@KellY#",
                    Occupation = "Manager",
                    Active = true,
                    EmailConfirmed = true,
                    UserTypeId = UserType.Manager,
                    UserRole = UserRole.Admin
                },

                new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Gideon",
                    LastName = "Gideon Smith",
                    UserName = "GideonSmith",
                    NormalizedUserName = "GIDEONSMITH",
                    Email = "gideon@gmail.com",
                    NormalizedEmail = "GIDEON@GMAIL.COM",
                    BirthDay = DateTime.UtcNow,
                    PhoneNumber = "09334028430842",
                    Password = "*Gideonite@Smith#",
                    Occupation = "Footballer",
                    Active = true,
                    EmailConfirmed =true,
                    UserTypeId = UserType.LandLord,
                    UserRole = UserRole.Admin,
                },

                new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Kennedy",
                    LastName = "bizz",
                    UserName = "BizzMan",
                    NormalizedUserName = "BIZZMAN",
                    Email = "bizz@gmail.com",
                    NormalizedEmail = "BIZZ@GMAIL.COM",
                    BirthDay = DateTime.UtcNow,
                    PhoneNumber = "09334028430842",
                    Password = "*BussinessMan@bizz#",
                    Occupation = "Bussiness man",
                    Active = true,
                    EmailConfirmed =true,
                    UserTypeId = UserType.Tenant,
                    UserRole = UserRole.User,
                }
            };

        }
    }
}

