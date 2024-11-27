using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.Interfaces;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class PagosController : Controller
{
    private readonly PayPalService _payPalService;

    public PagosController(PayPalService payPalService)
    {
        _payPalService = payPalService;
    }

    [HttpPost]
    public async Task<IActionResult> Pagar(decimal total)
    {
        var linkPago = await _payPalService.CrearOrden(total);
        return Redirect(linkPago); 
    }

    public async Task<IActionResult> Confirmacion(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            ViewBag.Mensaje = "No se recibió un token válido. Por favor, intenta nuevamente.";
            return View("Error");
        }

        try
        {
            var accessToken = await _payPalService.ObtenerToken(); 

            var orderDetails = await _payPalService.ObtenerDetallesOrden(token, accessToken);
            string orderId = orderDetails.GetProperty("id").GetString();
            var resultadoCaptura = await _payPalService.CapturarOrden(orderId);

            ViewBag.Mensaje = "Pago completado correctamente";
            return View("Confirmacion");
        }
        catch (Exception ex)
        {
            ViewBag.Mensaje = "Hubo un error al capturar el pago: " + ex.Message;
            return View("Error"); 
        }
    }



}
