using Disaster.BusinessLogic;
using Disaster.DataAccess.Context;
using Disaster.DataAccess.Manager;
using Disaster.DataAccess.Repository;
using Disaster.Mqtt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSCD.Model.Configration;
using RSCD.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster
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

        public static IServiceCollection AddReportDisasterServices(this IServiceCollection services)
        {
            services.AddScoped<DisasterReport_BL>();
            services.AddScoped<IReportedDisasterCollection, ReportDisaster_CM>();
            return services;
        }

        public static IServiceCollection AddMongoServices(this IServiceCollection services)
        {
            services.AddScoped<DB_Context>();
            return services;
        }
    }
}
