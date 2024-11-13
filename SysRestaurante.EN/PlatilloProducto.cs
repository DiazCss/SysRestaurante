using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysRestaurante.EN
{
    public class PlatilloProducto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdPlatillo { get; set; }

        [Required]
        public int IdProducto { get; set; }

        public decimal CantidadUsada { get; set; }

        
      
        public  Platillo Platillo { get; set; }

        
       
        public Producto Producto { get; set; }
    }
}
