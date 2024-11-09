using Microsoft.EntityFrameworkCore;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    public class SysRestauranteDbContext : DbContext
    {
        public SysRestauranteDbContext(DbContextOptions<SysRestauranteDbContext> options) : base(options){}

        public DbSet<DatosPersonales> datosPersonales { get; set; }
        public DbSet<Mesas> mesas { get; set; }

        public DbSet<Rol> roles { get; set; }

        public DbSet<Usuarios> usuario { get; set; }

        public DbSet<Empleado> empleado { get; set; }

        public DbSet<Compra> compras { get; set; }
        public DbSet<DetalleCompra> detallecompra { get; set; }
        public DbSet<Proveedor> proveedor { get; set; }
         public DbSet<Producto> producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empleado>()
                .HasOne(f => f.DatosPersonal)
                .WithMany(p => p.Empleados)
                .HasForeignKey(f => f.Id);


            modelBuilder.Entity<Usuarios>()
           .HasOne(f => f.datosPersonales)
           .WithMany(p => p.Usuarios)
           .HasForeignKey(f => f.IdDatosPersonales);

            modelBuilder.Entity<Usuarios>()
               .HasOne(u => u.rol)
               .WithMany()
               .HasForeignKey(u => u.IdRol);

            modelBuilder.Entity<Rol>().ToTable("roles");
            modelBuilder.Entity<Rol>().HasKey(s => s.Id);


            modelBuilder.Entity<Compra>()
           .HasMany(f => f.DetalleCompras)
           .WithOne(p => p.Compras) // Asegúrate de que la propiedad de navegación en DetalleCompra se llame "Compra"
            .HasForeignKey(f => f.IdCompra); // CompraId debe existir en la clase DetalleCompra


            // modelBuilder.Entity<DetalleCompra>()
            // .HasMany(p => p.Productos)
            // .WithOne(p => p.detalleCompra) // Asegúrate de que la propiedad de navegación en Producto se llame "DetalleCompra"
            // .HasForeignKey(f => f.Id); // DetalleCompraId debe existir en la clase Producto


            // modelBuilder.Entity<Compra>()
            //     .HasMany(f => f.DetalleCompra)  // Compra tiene muchos DetalleCompra
            //     .WithOne(p => p.Compra)         // DetalleCompra pertenece a una sola Compra
            //     .HasForeignKey(f => f.CompraId);  // La clave foránea está en DetalleCompra

        }

    }
}

