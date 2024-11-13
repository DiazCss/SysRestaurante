using SysRestaurante.BL.DTOs;

namespace SysRestaurante.BL.DTOs.PlatilloImagenDTOs
{
    public class PlatilloImagenBuscarDTO : PaginacionInputDTO 
    {
        public string NombrePlatillo_Like { get; set; } 
    }
}
