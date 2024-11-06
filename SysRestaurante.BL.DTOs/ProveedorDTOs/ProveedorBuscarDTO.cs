using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.ProveedorDTOs
{
    public class ProveedorBuscarDTO : PaginacionInputDTO
    {
        public string Nombre_Proveedor_Like { get; set; }
        public string Contacto_Proveedor_Like { get; set; }
        public string Direccion_Proveedor_Like { get; set; }
    }
}
