using System;

namespace SysRestaurante.EN;

public class Producto
{
    public int Id {get; set;}

    public string Nombre {get; set;}
    public string ContenidoEmpaque {get; set;}
    public int IdCategoriaProducto {get; set;}
    public string? Codigo { get; set; }
    
    
    public ICollection<Inventario> Inventarios { get; set; }
      public CategoriaProducto CategoriaProducto { get; set; } 

}
