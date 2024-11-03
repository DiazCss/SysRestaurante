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

        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Platillo> platillo { get; set; }
        public DbSet<Empleado> empleado { get; set; }
        public DbSet<Inventario> inventario { get; set; }
        public DbSet<Factura> factura { get; set; }
        public DbSet<DetalleFactura> detallefactura { get; set; }
        public DbSet<Mesa> mesas { get; set; }
        public DbSet<Pedido> pedido { get; set; }
        public DbSet<PedidoPlatillo> pedidoplatillo { get; set; }
        public DbSet<Proveedor> proveedor { get; set; }
        public DbSet<PlatilloImagen> platillo_imagen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Pedido)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.PedidoId);

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Factura)
                .WithMany(f => f.DetalleFacturas)
                .HasForeignKey(df => df.FacturaId);

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Platillo)
                .WithMany(p => p.DetalleFacturas)
                .HasForeignKey(df => df.PlatilloId);

            modelBuilder.Entity<PlatilloImagen>()
                .HasOne(pi => pi.Platillo)
                .WithMany(p => p.PlatilloImagenes)
                .HasForeignKey(pi => pi.PlatilloId);
        }

    }
}

