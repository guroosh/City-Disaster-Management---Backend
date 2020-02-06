using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RSCD.MQTT;
using RSCD;
using Registration.DataEntry.DataAccess.Context;
using RSCD.Middleware;
using Registration.BusinessLogic;
using Registration.DataAccess.Repository;
using Registration.DataAccess.Manager;

namespace Registration
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment,IConfiguration configuration)
        {
            Configuration = configuration;
            _hostEnvoirment = hostEnvironment.EnvironmentName;
        }

        public IConfiguration Configuration { get; }
        private string _hostEnvoirment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddModuleConfigurations(Configuration,_hostEnvoirment);
            services.AddScoped<DB_Context>();
            services.AddScoped<Registration_BL>();
            services.AddScoped<IUsersCollection,Redistration_CM>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMvc();
        }
    }
}
