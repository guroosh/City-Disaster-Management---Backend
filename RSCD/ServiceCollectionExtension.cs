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
            string deviceId = configuration.GetSection($"DeviceId").Value;
            Console.WriteLine(deviceId);
            Console.WriteLine((env == "Development") ? configuration.GetSection($"MongoConnection:{env}:ConnectionString:{deviceId}").Value : configuration.GetSection($"MongoConnection:{env}:ConnectionString").Value);

            services.Configure<DB_Settings>(options =>
            {
                options.DE_ConnectionString = (env == "Development") ? configuration.GetSection($"MongoConnection:{env}:ConnectionString:{deviceId}").Value : configuration.GetSection($"MongoConnection:{env}:ConnectionString").Value;
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
