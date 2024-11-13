using System;
using SysRestaurante.BL.DTOs; 

namespace SysRestaurante.BL.DTOs.PlatilloProductoDTOs
{
    public class PlatilloProductoBuscarDTO : PaginacionInputDTO 
    {
        public int? IdPlatillo_Equal { get; set; }
        public int? IdProducto_Equal { get; set; }
        public decimal? CantidadUsada_GreaterThanOrEqual { get; set; }
        public decimal? CantidadUsada_LessThanOrEqual { get; set; }
         public string NombrePlatillo_Like { get; set; }
    public string NombreProducto_Like { get; set; }
    }
}
