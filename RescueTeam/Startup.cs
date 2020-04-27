using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RSCD;

namespace RescueTeam
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
            services.AddModuleConfigurations(Configuration, _hostEnvoirment);
            services.AddMqttServices(Configuration);
            services.AddMongoServices();
            services.AddOfficerDetailsServices();
            services.AddVerifiedDisasterReportServices();
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
