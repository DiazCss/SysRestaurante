using System;
using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs.InventarioDTOs;

public class InventarioBuscarDTO : PaginacionInputDTO
{
    public string IdProducto_Inventario_Like { get; set; }
    [Display(Name = "Producto")]
    public int IdProducto_equal { get; set; }

}
