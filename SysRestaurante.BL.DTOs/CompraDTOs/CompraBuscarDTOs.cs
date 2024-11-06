using System;

namespace SysRestaurante.BL.DTOs.CompraDTOs;

public class CompraBuscarDTOs : PaginacionInputDTO
{
        public string NumeroFactura_Compra_Like { get; set; }
        public DateTime? Fecha_Compra_Like { get; set; }
        
}
