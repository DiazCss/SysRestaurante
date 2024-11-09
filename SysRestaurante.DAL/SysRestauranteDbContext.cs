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
        public DbSet<Proveedor> proveedor { get; set; }

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

        }

    }
}

