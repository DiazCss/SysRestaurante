using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.PlatilloDTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace SysRestaurante.BL.DTOs.PlatilloDTOs
    {
        public class PlatilloIndexDTO
        {
            public int Id { get; set; } 
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public string Disponibilidad { get; set; }
            public string Categoria { get; set; }
        }

    }

}
