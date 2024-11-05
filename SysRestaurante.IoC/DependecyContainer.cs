using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SysRestaurante.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.IoC
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddIoCDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDALDependecies(configuration);
            //services.AddMockDependecies();
            return services;
        }
    }
}
