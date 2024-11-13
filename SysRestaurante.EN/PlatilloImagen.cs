using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysRestaurante.EN
{
    public class PlatilloImagen
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Platillo")]
        public int? IdPlatillo { get; set; }

        public string ImagenPlatillo { get; set; } 

        public Platillo Platillo { get; set; } 
    }
}
