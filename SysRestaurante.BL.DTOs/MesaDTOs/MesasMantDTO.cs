using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.MesaDTOs
{
    public class MesasMantDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NumeroMesa  { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Capacidad { get; set; }
        [Required]
        public byte Disponibilidad { get; set; }
    }
}
