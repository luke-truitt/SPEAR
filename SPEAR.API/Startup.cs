using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProValens.Enterprise.Portals.API.Configuration;
using SPEAR.Configuration;
using SPEAR.Configuration.StartupConfiguration;

namespace SPEAR
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureServices()
                .ConfigureBusinessServices()
                .ConfigureDataServices(Configuration)
                .ConfigureCors(Configuration)
                .ConfigureSwagger()
                .AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            if (env.IsDevelopment() || env.IsEnvironment("Local") || env.IsEnvironment("Dev") || env.IsEnvironment("QA"))
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .RegisterServices()
                .RegisterSwagger()
                .RegisterCors(); 

            app
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseMvc();

        }
    }
}
