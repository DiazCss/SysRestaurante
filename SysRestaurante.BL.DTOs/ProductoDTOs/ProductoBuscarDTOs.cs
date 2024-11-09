using System;

namespace SysRestaurante.BL.DTOs.ProductoDTOs;

public class ProductoBuscarDTOs : PaginacionInputDTO
{
        public string Nombre_Producto_Like { get; set; }
        public int? IdCategoriaProducto_Compra_Like { get; set; }
}
