using Gateway.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RSCD;

namespace Gateway
{
    public class Startup
    {
        public Startup(IHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            _hostingEnvoirment = environment.EnvironmentName;
        }

        public IConfiguration Configuration { get; }
        private string _hostingEnvoirment { get; }
        private readonly string _crosPolicy = "RSCDPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRscdCorsPolicy(_crosPolicy);
            services.AddAuthenticationConfigurations(Configuration);
            services.AddModuleConfigurations(Configuration, _hostingEnvoirment);
            services.AddRoutingServices();
            services.AddMongoSerivces();
            services.AddLoginServices();
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
