using RSCD.Model.Configration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace RSCD
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddModuleConfigurations(this IServiceCollection services, IConfiguration configuration, string env)
        {
            services.Configure<DB_Settings>(options =>
            {
                options.DE_ConnectionString = configuration.GetSection($"MongoConnection:{env}:ConnectionString").Value;
                options.DE_DataBase = configuration.GetSection($"MongoConnection:{env}:Database").Value;
            });

            try
            {
                services.Configure<Token>(options =>
                {
                    options.Key = configuration.GetSection("ModuleToken:Key").Value;
                    options.Value = configuration.GetSection("ModuleToken:Value").Value;
                });
            }
            catch { }
            
            return services;
        }

    }
}
