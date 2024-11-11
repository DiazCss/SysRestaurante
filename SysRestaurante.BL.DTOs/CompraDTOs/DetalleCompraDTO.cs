using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.CompraDTOs
{
    public class DetalleCompraDTO : ActionsDTO
    {
        public int Id { get; set; }

        public int IdProducto { get; set; }
        public int IdCompra { get; set; }

        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public Producto? Producto { get; set; }

    }
}
