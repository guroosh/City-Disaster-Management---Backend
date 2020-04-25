using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RescueTeam.BusinessLogic;
using RescueTeam.DataAccess.Context;
using RescueTeam.DataAccess.Manager;
using RescueTeam.DataAccess.Repository;
using RescueTeam.Mqtt;
using RSCD.Model.Configration;
using RSCD.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMqttServices(this IServiceCollection services, IConfiguration configuration)
        {
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

        public static IServiceCollection AddOfficerDetailsServices(this IServiceCollection services)
        {
            services.AddScoped<RescueTeam_BL>();
            services.AddScoped<IOfficerDetailCollection, RescueTeam_CM>();
            return services;
        }

        public static IServiceCollection AddVerifiedDisasterReportServices(this IServiceCollection services)
        {
            services.AddScoped<IVerifiedDisasterReportCollection, VerifiedDisasterReport_CM>();
            return services;
        }

        public static IServiceCollection AddMongoServices(this IServiceCollection services)
        {
            services.AddScoped<DB_Context>();
            return services;
        }
    }
}

