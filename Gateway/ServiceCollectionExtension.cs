using Gateway.BusinessLogic;
using Gateway.DataAccess;
using Gateway.Handlers.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSCD.Model.Configration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddAuthenticationConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Auth_Config>(options =>
            {
                options.IssuedTo = configuration.GetSection("AuthConfiguration:IssuedTo").Value;
                options.Issuer = configuration.GetSection("AuthConfiguration:Issuer").Value;
                options.L1Key = configuration.GetSection("AuthConfiguration:L1Key").Value;
                options.L1Token = configuration.GetSection("AuthConfiguration:L1Token").Value;
                options.L2Token = configuration.GetSection("AuthConfiguration:L2Token").Value;
                options.L3Token = configuration.GetSection("AuthConfiguration:L3Token").Value;
                options.PayLoadKey = configuration.GetSection("AuthConfiguration:PayLoadKey").Value;
                options.SecurityKey = configuration.GetSection("AuthConfiguration:SecurityKey").Value;
            });
            return services;
        }

        public static IServiceCollection AddRscdCorsPolicy(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, p => p.WithHeaders("RSCD-Token", "rscd-jwt-token", "content-type", "channel")
                .AllowAnyMethod()
                .AllowAnyOrigin());
            });

            return services;
        }

        public static IServiceCollection AddMongoServices(this IServiceCollection services)
        {
            services.AddScoped<DB_Context>();
            return services;
        }

        public static IServiceCollection AddLoginServices(this IServiceCollection services)
        {
            services.AddScoped<Login_BL>();
            return services;
        }

        public static IServiceCollection AddRoutingServices(this IServiceCollection services)
        {
            services.AddSingleton<RoutingManager>();
            services.AddScoped<RequestRedirectManager>();
            return services;
        }
    }
}
