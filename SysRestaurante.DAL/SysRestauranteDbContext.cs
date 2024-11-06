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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empleado>()
                .HasOne(f => f.DatosPersonal)
                .WithMany(p => p.Empleados)
                .HasForeignKey(f => f.Id);
        }

    }
}

