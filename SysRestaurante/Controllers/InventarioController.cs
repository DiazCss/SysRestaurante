using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.InventarioDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

    public class InventarioController : Controller
    {
        readonly IInventarioBL inventarioBL;
        readonly IProductoBL productoBL;

        public InventarioController(IInventarioBL pInventarioBL, IProductoBL pProductoBL)
        {
            inventarioBL = pInventarioBL;
            productoBL = pProductoBL;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        // GET: InventarioController
        public async Task<ActionResult> Index(InventarioBuscarDTO pInventario = null)
        {
            if (pInventario == null)
                pInventario = new InventarioBuscarDTO();
            if (pInventario.Take == 0)
                pInventario.Take = 10;
            var paginacion = await inventarioBL.BuscarAsync(pInventario);
            ViewBag.productos = await productoBL.ObtenerTodosAsync();
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
                ViewBag.producto = await productoBL.ObtenerTodosAsync();
                ViewBag.ActionsUI = pAccion;
                ViewBag.Error = "";
                InventarioMantDTO inventarioMantDTO = new InventarioMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        inventarioMantDTO = await inventarioBL.ObtenerPorIdAsync(new InventarioMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(inventarioMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Create(InventarioMantDTO pInventario)
{
    try
    {
        int result = await inventarioBL.CreateAsync(pInventario);
        TempData["Mensaje"] = "Inventario creado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear el inventario: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Edit(InventarioMantDTO pInventario)
{
    try
    {
        int result = await inventarioBL.ModificarAsync(pInventario);
        TempData["Mensaje"] = "Inventario editado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar el inventario: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Delete(InventarioMantDTO pInventario)
{
    try
    {
        int result = await inventarioBL.EliminarAsync(pInventario);
        TempData["Mensaje"] = "Inventario eliminado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar el inventario: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Detail(InventarioMantDTO pInventario)
        {
            return RedirectToAction(nameof(Mant), new { id = pInventario.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }




        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]
        public async Task<IActionResult> ExportarReporteInventario()
        {
            try
            {
                // Obtén los datos del inventario desde la lógica de negocio
                var inventarios = await inventarioBL.ObtenerTodosAsync();

                // Verifica que haya datos disponibles
                if (inventarios == null || !inventarios.Any())
                {
                    TempData["Mensaje"] = "No hay inventarios para generar el reporte.";
                    return RedirectToAction("Index");
                }

                // Crear un nuevo libro de Excel
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("ReporteInventario");

                // Sección de encabezados
                worksheet.Cell("B1").Value = "REPORTE DE INVENTARIO";
                worksheet.Cell("B2").Value = "Generado el:";
                worksheet.Cell("C2").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Encabezados de la tabla
                worksheet.Cell("B4").Value = "Producto";
                worksheet.Cell("C4").Value = "Cantidad Disponible";
                worksheet.Cell("D4").Value = "Cantidad Mínima";
                worksheet.Cell("E4").Value = "Costo Unitario";
                worksheet.Cell("F4").Value = "Fecha Última Compra";
                worksheet.Cell("G4").Value = "Fecha Caducidad Lote";

                // Formato de los encabezados
                var headerRange = worksheet.Range("B4:G4");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#BF9000");
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                // Llenar datos del inventario
                int row = 5; // Comienza después del encabezado
                foreach (var inventario in inventarios)
                {
                    worksheet.Cell(row, 2).Value = inventario.producto.Nombre;
                    worksheet.Cell(row, 3).Value = inventario.CantidadDisponible;
                    worksheet.Cell(row, 4).Value = inventario.CantidadMinima;
                    worksheet.Cell(row, 5).Value = inventario.CostoUnitario.ToString("C2"); // Formato moneda
                    worksheet.Cell(row, 6).Value = inventario.FechaUltimaCompra.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 7).Value = inventario.FechaCaducidadLote.ToString("dd/MM/yyyy");

                    // Aplicar bordes a cada fila
                    var dataRange = worksheet.Range($"B{row}:G{row}");
                    dataRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    row++;
                }

                // Ajustar las columnas al contenido
                worksheet.Columns().AdjustToContents();

                // Guardar el archivo en un MemoryStream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    // Devolver el archivo Excel
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error al exportar el reporte.";
                // Log del error
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }


    }
}