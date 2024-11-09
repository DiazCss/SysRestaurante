using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddDALDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SysRestauranteDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("Conn"), ServerVersion.AutoDetect(configuration.GetConnectionString("Conn"))));
            services.AddScoped<IEmpleadoBL, EmpleadoDAL>();
            services.AddScoped<IMesasBL, MesasDAL>();
            services.AddScoped<IRolBL, RolDAL>();
            services.AddScoped<IComprasBL, CompraDAL>();
            services.AddScoped<IUsuarioBL, UsuarioDAL>();
            services.AddScoped<IProveedorBL, ProveedorDAL>();
            services.AddScoped<IProductoBL, ProductoDAL>();


            return services;
        }
    }
}
