using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registration.BusinessLogic;
using Registration.DataAccess.Manager;
using Registration.DataAccess.Repository;
using Registration.DataEntry.DataAccess.Context;
using Registration.Mqtt;
using RSCD.Model.Configration;
using RSCD.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMqttServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Mqtt_Settings>(options =>
            {
                options.ClientId = "RSCD_RegistrationModule";
                options.Host = "localhost";
                options.SuscribeTopic = "RSCD/Registration/#";
            });
            services.Configure<Mqtt_Settings>(options =>
            {
                options.ClientId = configuration.GetSection("AuthConfiguration:IssuedTo").Value;
                options.Host = configuration.GetSection("AuthConfiguration:IssuedTo").Value;
                options.SuscribeTopic = configuration.GetSection("AuthConfiguration:IssuedTo").Value;
            });
            services.AddHostedService<MqttSubscriber>();
            services.AddScoped<MqttPublisher>();
            return services;
        }

        public static IServiceCollection AddRegistartionServices(this IServiceCollection services)
        {
            services.AddScoped<Registration_BL>();
            services.AddScoped<IUsersCollection, Redistration_CM>();
            return services;
        }  

        public static IServiceCollection AddMongoServices(this IServiceCollection services)
        {
            services.AddScoped<DB_Context>();
            return services;
        }
    }
}
