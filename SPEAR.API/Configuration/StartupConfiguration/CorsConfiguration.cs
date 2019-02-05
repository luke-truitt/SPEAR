using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProValens.Enterprise.Portals.API.Configuration
{
    public static class CorsConfiguration
    {
        private const string PolicyName = "Spear.Cors";
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName,
                    builder => builder
                        // .WithOrigins("http://localhost:4200") //TODO: Get from appSettings
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Total-Count")
                        .AllowCredentials());
            });
            return services;
        }

        public static IApplicationBuilder RegisterCors(this IApplicationBuilder app)
        {
            app.UseCors(PolicyName);
            return app;
        }



    }
}
