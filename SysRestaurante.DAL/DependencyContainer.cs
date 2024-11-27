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
            // Registra IHttpClientFactory para su uso en otros servicios
            services.AddHttpClient();

            // Configura el DbContext para la base de datos
            services.AddDbContext<SysRestauranteDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("Conn"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("Conn"))));

            // Registra las dependencias de la capa DAL (Data Access Layer)
            services.AddScoped<IEmpleadoBL, EmpleadoDAL>();
            services.AddScoped<IMesasBL, MesasDAL>();
            services.AddScoped<IRolBL, RolDAL>();
            services.AddScoped<IComprasBL, CompraDAL>();
            services.AddScoped<IUsuarioBL, UsuarioDAL>();
            services.AddScoped<IProveedorBL, ProveedorDAL>();
            services.AddScoped<IProductoBL, ProductoDAL>();
            services.AddScoped<ICategoriaProductoBL, CategoriaProductoDAL>();
            services.AddScoped<IInventarioBL, InventarioDAL>();
            services.AddScoped<ICategoriaPlatilloBL, CategoriaPlatilloDAL>();
            services.AddScoped<IPlatilloBL, PlatilloDAL>();
            services.AddScoped<IPlatilloProductoBL, PlatilloProductoDAL>();
            services.AddScoped<IPlatilloImagenBL, PlatilloImagenDAL>();
            services.AddScoped<IFacturaBL, FacturaDAL>();

            // Registra el servicio de PayPal
            services.AddScoped<PayPalService>();

            return services;
        }
    }
}
