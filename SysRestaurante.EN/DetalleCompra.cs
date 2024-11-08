using System;
using System.Security.Cryptography;

namespace SysRestaurante.EN;

public class DetalleCompra
{
    public int Id {get; set;}
    public int IdCompra {get; set;}
    public int IdProducto {get; set;}
    public decimal PrecioUnitario {get; set;}
    public int Cantidad {get; set;}
    public decimal SubTotal {get; set;}
    
    public Compra Compras {get; set;}
    // public Producto Productos {get; set;}
    public ICollection<Producto> Productos {get; set;}
    // public List<Compra> Compras {get; set;}
}
