using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class PlatilloImagen
    {
        public int Id { get; set; }
        public int PlatilloId { get; set; }
        public byte[] Imagen { get; set; }

        public virtual Platillo Platillo { get; set; }
    }


}
