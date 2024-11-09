using System;

namespace SysRestaurante.EN;

public class Producto
{
    public int Id {get; set;}

    public string Nombre {get; set;}
    public string ContenidoEmpaque {get; set;}
    public int IdCategoriaProducto {get; set;}
    
    public ICollection<DetalleCompra> detalleCompra {get; set;}
}
