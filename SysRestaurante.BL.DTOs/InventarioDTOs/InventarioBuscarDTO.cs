using System;

namespace SysRestaurante.BL.DTOs.InventarioDTOs;

public class InventarioBuscarDTO : PaginacionInputDTO
{
    public string IdProducto_Inventario_Like { get; set; }

}
