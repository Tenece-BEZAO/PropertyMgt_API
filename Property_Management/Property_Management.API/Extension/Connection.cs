using Microsoft.EntityFrameworkCore;
using Property_Management.DAL.Context;

namespace Property_Management.API.Extension
{
    public static class Connection
    {
        public static void AddConnection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<PMSDbContext>(dbOption =>
            {
                var ConnectionString = builder.Configuration.GetSection("ConnectionStrings")["ConnString"];
                dbOption.UseSqlServer(ConnectionString);
            });
        }
    }
}
