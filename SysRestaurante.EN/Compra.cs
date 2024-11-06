using System;

namespace SysRestaurante.EN;

public class Compra
{
    public int Id {get; set;}
    public string NumeroFactura {get; set;}
    public DateTime Fecha {get; set;}
    public decimal Iva {get; set;}
    public decimal Total {get; set;}
    public int IdProveedor {get; set;}


}
