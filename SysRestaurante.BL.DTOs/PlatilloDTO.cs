using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class PlatilloDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Required]
        public byte Disponibilidad { get; set; } 

        [Required]
        public TimeSpan TiempoPreparacion { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El ingrediente principal no puede exceder los 50 caracteres.")]
        public string IngredientePrincipal { get; set; }

        [Required]
        public DateTime FechaActualizacion { get; set; }

        
        public virtual ICollection<DetalleFacturaDTO> DetalleFacturas { get; set; }
        public virtual ICollection<PedidoPlatilloDTO> PedidoPlatillos { get; set; }
        public virtual ICollection<PlatilloImagenDTO> PlatilloImagenes { get; set; }
    }
}
