using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class PlatilloImagenDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int PlatilloId { get; set; }

        [Required]
        [MaxLength(int.MaxValue)] // Limitación técnica, ya que el tamaño real depende de la implementación
        public byte[] Imagen { get; set; }
    }
}
