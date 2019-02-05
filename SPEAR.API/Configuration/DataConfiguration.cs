using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SPEAR.Data;
using SPEAR.Data.Repositories;

namespace SPEAR.Configuration
{
    public static class DataConfiguration
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IResourceRepository, ResourceRepository>();
            return services.ConfigureDatabase(configuration);
        }

        private static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ResourceContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"),
                sqlServerOptions => sqlServerOptions.CommandTimeout(600))
            );

            try
            {
                var context = services.GetService<ResourceContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetService<ILogger>();
                logger?.LogError(ex.Message);
            }

            return services;
        }

        private static TType GetService<TType>(this IServiceCollection services)
        {
            var temporaryProvider = services.BuildServiceProvider();
            var service = temporaryProvider.GetService<TType>();
            return service;
        }
    }
}
