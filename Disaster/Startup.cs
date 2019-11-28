using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disaster.BusinessLogic;
using Disaster.DataAccess.Manager;
using Disaster.DataAccess.Repository;
using Disaster.Mqtt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RSCD.Model.Configration;
using RSCD.Mqtt;
using RSCD;
using Disaster.DataEntry.DataAccess.Context;
using RSCD.Middleware;

namespace Disaster
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment,IConfiguration configuration)
        {
            Configuration = configuration;
            _hostEnvoirment = hostEnvironment.EnvironmentName;
        }

        public IConfiguration Configuration { get; }
        private string _hostEnvoirment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Mqtt_Settings>(options =>
            {
                options.ClientId = "RSCD_DisasterModule";
                options.Host = "10.6.38.181";
                options.SuscribeTopic = "RSCD/Disaster/#";
            });

            services.AddModuleConfigurations(Configuration, _hostEnvoirment);
            services.AddScoped<DB_Context>();
            services.AddHostedService<MqttSubscriber>();
            services.AddScoped<MqttPublisher>();
            services.AddScoped<DisasterReport_BL>();
            services.AddScoped<IReportedDisasterCollection, ReportDisaster_CM>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMvc();
        }
    }
}
