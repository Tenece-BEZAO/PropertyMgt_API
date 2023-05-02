using Property_Management.API.Extension;
using Property_Management.DAL.SeedData;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCustomServices();
            builder.Services.AddAutoMapper(Assembly.Load("Property_Management.BLL"));
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder);
            builder.Services.AddConnection(builder);
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.ConfigureExceptionHandler(builder.Environment);

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
           // await Seed.EnsurePopulatedAsync(app);
            await app.RunAsync();
        }
    }
}