using Gateway.BusinessLogic;
using Gateway.DataAccess;
using Gateway.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RSCD.Model.Configration;
using Gateway.Mqtt;

namespace Gateway
{
    public class Startup
    {
        public Startup(IHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            _hostingEnvironment = environment.EnvironmentName;
        }

        public IConfiguration Configuration { get; }
        private string _hostingEnvironment { get; }
        private readonly string _crosPolicy = "RSCDPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Mqtt_Settings>(options =>
            {
                options.ClientId = "RSCD_GatewayModule";
                options.Host = "10.6.32.103";
                options.SuscribeTopic = "RSCD/Registration/#";
            });

            services.AddRscdCorsPolicy(_crosPolicy);
            services.AddAuthenticationConfigurations(Configuration);
            services.AddScoped<DB_Context>();
            services.AddHostedService<MqttSubscriber>();
            services.AddScoped<Login_BL>();
            services.AddRoutingServices();
            services.AddMvc(options => options.EnableEndpointRouting = false);  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(_crosPolicy);
            app.UseMiddleware<Authentication>();
            app.UseMiddleware<Router>();
            app.UseMvc();
        }
    }
}
