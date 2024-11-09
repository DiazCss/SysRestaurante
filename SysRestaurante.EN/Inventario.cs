using System;

namespace SysRestaurante.EN;

public class Inventario
{
    public int Id { get; set; }
    public int IdProducto { get; set; }
    public int CantidadDisponible { get; set; }
    public int CantidadMinima { get; set; }
    public decimal CostoUnitario { get; set; }
    public DateTime FechaUltimaCompra { get; set; }
    public DateTime FechaCaducidadLote { get; set; }

    public Producto productos { get; set; }

}
