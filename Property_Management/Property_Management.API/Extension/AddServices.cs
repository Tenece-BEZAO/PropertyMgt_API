﻿using Property_Management.BLL.Implementations;
using Property_Management.BLL.Infrastructure.jwt;
using Property_Management.BLL.Infrastructures.jwt;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Context;
using Property_Management.DAL.Implementations;
using Property_Management.DAL.Interfaces;

namespace Property_Management.API.Extension
{
    public static class AddServices
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAuth, UserAuth>();
            services.AddScoped<ITenantServices, TenantServices>();
            services.AddScoped<IMaintenanceRequestServices, MaintenaceRequestServices>();
            services.AddScoped<IMangerServices, ManagerServices>();
            services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddTransient<IJWTAuthenticator, JwtAuthenticator>();
            services.AddTransient<JwtConfig, JwtConfig>();
            services.AddTransient<IUnitOfWork, UnitOfWork<PMSDbContext>>();
            services.AddTransient<IServiceFactory, ServiceFactory>();
            services.AddTransient<IRoleService, RoleService>();
        }
    }
}
