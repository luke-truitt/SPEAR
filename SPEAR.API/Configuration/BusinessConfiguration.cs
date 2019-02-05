using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SPEAR.Data.Repositories;
using SPEAR.Services;

namespace SPEAR.Configuration
{
    public static class BusinessConfiguration
    {
        public static IServiceCollection ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IPersonnelService, PersonnelService>();

            return services;
        }
    }
}
