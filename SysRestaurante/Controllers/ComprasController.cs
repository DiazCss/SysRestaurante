using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.CompraDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System.Drawing;
using System.Security.Claims;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

    public class CompraController : Controller
    {
        readonly IComprasBL compraBL;
        readonly IProductoBL productoBL;
        readonly IProveedorBL proveedorBL;

        public CompraController(IComprasBL pCompraBL, IProductoBL pProductoBL, IProveedorBL pProveedorBL)
        {
            compraBL = pCompraBL;
            productoBL = pProductoBL;
            proveedorBL = pProveedorBL;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        // GET: CompraController
        public async Task<IActionResult> Index(CompraBuscarDTOs pCompra = null)
        {
            if (pCompra == null)
                pCompra = new CompraBuscarDTOs();
            if (pCompra.Take == 0)
                pCompra.Take = 10;
            var paginacion = await compraBL.BuscarAsync(pCompra);
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View(paginacion.Data);
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        public async Task<IActionResult> Mant(int id, ActionsUI pAccion)
{
    if (pAccion.EsValidAction())
    {
        ViewBag.ActionsUI = pAccion;
        ViewBag.Error = "";

        
        CompraManDTOs compraMantDTO = new CompraManDTOs();

        try
        {
            
            var proveedores = await proveedorBL.ObtenerTodosAsync(); 
            ViewBag.Proveedores = proveedores;

           
            if (pAccion.SiTraerDatos())
            {
                compraMantDTO = await compraBL.ObtenerPorIdAsync(new CompraManDTOs { Id = id });
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
        }

        return View(compraMantDTO);
    }
    else
    {
        return RedirectToAction(nameof(Index));
    }
}

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(CompraManDTOs pCompra)
{
    try
    {
        int result = await compraBL.CreateAsync(pCompra);
        TempData["Mensaje"] = "Compra creada exitosamente.";
        TempData["TipoMensaje"] = "success"; 
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear la compra: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(CompraManDTOs pCompra)
{
    try
    {
        int result = await compraBL.ModificarAsync(pCompra);
        TempData["Mensaje"] = "Compra editada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar la compra: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(CompraManDTOs pCompra)
{
    try
    {
        int result = await compraBL.EliminarAsync(pCompra);
        TempData["Mensaje"] = "Compra eliminada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar la compra: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(CompraManDTOs pCompra)
        {
            return RedirectToAction(nameof(Mant), new { id = pCompra.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }

           public async Task<IActionResult>AutoCompleteProducto(string query)
        {
            var list = await productoBL.AutoCompleteProducto(query);
            return Json(list);
        }





        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]
        public async Task<IActionResult> ExportarReporteCompras()
        {
            try
            {
                // Obtener los datos de compras desde la lógica de negocio
                var compras = await compraBL.ObtenerTodosAsync();

                // Verificar si hay datos disponibles
                if (compras == null || !compras.Any())
                {
                    TempData["Mensaje"] = "No hay compras disponibles para generar el reporte.";
                    return RedirectToAction(nameof(Index));
                }

                // Crear un nuevo libro de Excel
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("ReporteCompras");

                // Título del reporte
                worksheet.Cell("B1").Value = "REPORTE DE COMPRAS";
                worksheet.Cell("B2").Value = "Generado el:";
                worksheet.Cell("C2").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Encabezados de la tabla
                worksheet.Cell("B4").Value = "ID";
                worksheet.Cell("C4").Value = "Número de Factura";
                worksheet.Cell("D4").Value = "Fecha";
                worksheet.Cell("E4").Value = "IVA";
                worksheet.Cell("F4").Value = "Total";
                worksheet.Cell("G4").Value = "Proveedor";

                // Formato de los encabezados
                var headerRange = worksheet.Range("B4:G4");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#4472C4");
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                // Llenar datos de compras
                int row = 5; // Comienza después del encabezado
                foreach (var compra in compras)
                {
                    worksheet.Cell(row, 2).Value = compra.Id;
                    worksheet.Cell(row, 3).Value = compra.NumeroFactura;
                    worksheet.Cell(row, 4).Value = compra.Fecha.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 5).Value = compra.Iva;
                    worksheet.Cell(row, 6).Value = compra.Total;
                    worksheet.Cell(row, 7).Value = compra.IdProveedor; // Puedes adaptar para mostrar el nombre del proveedor si está disponible
                    row++;
                }

                // Ajustar columnas al contenido
                worksheet.Columns().AdjustToContents();

                // Guardar el archivo en un MemoryStream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    // Devolver el archivo Excel
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Compras_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error al exportar el reporte.";
                // Log del error
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


    }
}
