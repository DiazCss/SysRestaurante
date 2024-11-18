using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.FacturaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IFacturaBL
    {
        public Task CrearFacturaAsync(FacturaDTO facturaDTO);

    }
}
