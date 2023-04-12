using Property_Management.API.Extension;
using Property_Management.DAL.DataSeeder;
using System.Reflection;

namespace Property_Management.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCustomServices();
            builder.Services.AddAutoMapper(Assembly.Load("Property_Management.BLL"));
            builder.Services.ConfigureSwaggerGen();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder);
            builder.Services.AddConnection(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.ConfigureExceptionHandler(builder.Environment);

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            await SeedAdmin.EnsurePopulatedAsync(app);
            await SeedLandLord.EnsurePopulatedAsync(app);
            await SeedTenant.EnsurePopulatedAsync(app);
            await SeedStaff.EnsurePopulatedAsync(app);
            await app.RunAsync();
        }
    }
}