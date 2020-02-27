using Microsoft.Extensions.DependencyInjection;
using Registration.BusinessLogic;
using Registration.DataAccess.Manager;
using Registration.DataAccess.Repository;
using Registration.DataEntry.DataAccess.Context;
using Registration.Mqtt;
using RSCD.Model.Configration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSCD_tests.Registration
{
    class Registration_BLTests
    {
     private void setupServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.Configure<Mqtt_Settings>(options =>
            {
                options.ClientId = "RSCD_RegistrationModule";
                options.Host = "localhost";
                options.SuscribeTopic = "RSCD/Registration/#";
            });
            services.Configure<DB_Settings>(options =>
            {
                options.DE_ConnectionString = "";
                options.DE_DataBase = "";
            });
            services.AddScoped<DB_Context>();
            services.AddHostedService<MqttSubscriber>();
            services.AddScoped<RSCD.Mqtt.MqttPublisher>();
            services.AddScoped<Registration_BL>();
            services.AddScoped<IUsersCollection, Registration_CM>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }
    }
    

}
