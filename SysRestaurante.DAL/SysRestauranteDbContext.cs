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
        public DbSet<CategoriaProducto> categoriaProducto { get; set; }
        public DbSet<Inventario> inventario { get; set; }
        public DbSet<CategoriaPlatillo> categoriaPlatillos { get; set; }
         public DbSet<Platillo> platillo {get; set;}
       public DbSet<PlatilloProducto> platilloProducto {get; set;}
       public DbSet<PlatilloImagen> platilloImagen {get; set;}
        public DbSet<Factura> factura { get; set; }
        public DbSet<DetalleFactura> detalleFactura { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empleado>()
                .HasOne(f => f.DatosPersonal)
                .WithMany(p => p.Empleados)
                .HasForeignKey(f => f.Id);
            // Configuración de la relación entre Compra y DetalleCompra
            modelBuilder.Entity<Compra>()
                .HasMany(f => f.DetalleCompras)
                .WithOne(p => p.Compras)  // Asegúrate de que la propiedad de navegación en DetalleCompra se llame "Compras"
                .HasForeignKey(f => f.IdCompra);  // IdCompra debe existir en DetalleCompra

// Configuración de la entidad DetalleCompra
            modelBuilder.Entity<DetalleCompra>().ToTable("detallecompra");
            modelBuilder.Entity<DetalleCompra>().HasKey(s => s.Id);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(s => s.Compras)
                .WithMany(g => g.DetalleCompras)
                .HasForeignKey(s => s.IdCompra);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(dc => dc.Producto)  // Cada detalle de compra tiene un producto
                .WithMany()  // Un producto puede estar en muchos detalles de compra
                .HasForeignKey(dc => dc.IdProducto)  // La clave foránea está en DetalleCompra
                .HasConstraintName("FK_DetalleCompra_Producto");

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

            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.productos)
                .WithMany(p => p.Inventarios)
                .HasForeignKey(i => i.IdProducto);
                  base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Platillo>()
        .HasOne(p => p.CategoriaPlatillos)
        .WithMany() 
        .HasForeignKey(p => p.IdCategoria) ;
        
          modelBuilder.Entity<PlatilloProducto>()
        .HasOne(pp => pp.Platillo)
        .WithMany()
        .HasForeignKey(pp => pp.IdPlatillo);

    modelBuilder.Entity<PlatilloProducto>()
        .HasOne(pp => pp.Producto)
        .WithMany()
        .HasForeignKey(pp => pp.IdProducto);
        
    modelBuilder.Entity<PlatilloImagen>()
        .HasOne(pp => pp.Platillo)
        .WithMany()
        .HasForeignKey(pp => pp.IdPlatillo);

        modelBuilder.Entity<Platillo>()
    .HasMany(p => p.PlatilloImagenes) // Un Platillo tiene muchas imágenes
    .WithOne(pi => pi.Platillo)      // Una imagen pertenece a un Platillo
    .HasForeignKey(pi => pi.IdPlatillo);
 
       
        
      
        }
        

    }
}

