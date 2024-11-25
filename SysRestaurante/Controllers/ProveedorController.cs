using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using SysRestaurante.BL.DTOs.ProveedorDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

    public class ProveedorController : Controller
    {
        readonly IProveedorBL proveedorBL;

        public ProveedorController(IProveedorBL pProveedorBL)
        {
            proveedorBL = pProveedorBL;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        // GET: ProveedorController
        public async Task<ActionResult> Index(ProveedorBuscarDTO pProveedor = null)
        {
            if (pProveedor == null)
                pProveedor = new ProveedorBuscarDTO();
            if (pProveedor.Take == 0)
                pProveedor.Take = 10;
            var paginacion = await proveedorBL.BuscarAync(pProveedor);
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
                ProveedorMantDTO proveedorMantDTO = new ProveedorMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        proveedorMantDTO = await proveedorBL.ObtenerPorIdAsync(new ProveedorMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(proveedorMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Create(ProveedorMantDTO pProveedor)
{
    try
    {
        int result = await proveedorBL.CreateAsync(pProveedor);
        TempData["Mensaje"] = "Proveedor creado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear el proveedor: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }

    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Edit(ProveedorMantDTO pProveedor)
{
    try
    {
        int result = await proveedorBL.ModificarAsync(pProveedor);
        TempData["Mensaje"] = "Proveedor editado exitosamente.";
        TempData["TipoMensaje"] = "success"; 
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar el proveedor: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }

    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Delete(ProveedorMantDTO pProveedor)
{
    try
    {
        int result = await proveedorBL.EliminarAsync(pProveedor);
        TempData["Mensaje"] = "Proveedor eliminado exitosamente.";
        TempData["TipoMensaje"] = "success"; 
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar el proveedor: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }

    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Detail(ProveedorMantDTO pProveedor)
        {
            return RedirectToAction(nameof(Index), new { id = pProveedor.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }




        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]
        public async Task<IActionResult> ExportarReporteProveedores()
        {
            try
            {
                // Obtener los datos de los proveedores desde la lógica de negocio
                var proveedores = await proveedorBL.ObtenerTodosAsync();

                // Verificar si hay datos disponibles
                if (proveedores == null || !proveedores.Any())
                {
                    TempData["Mensaje"] = "No hay proveedores disponibles para generar el reporte.";
                    return RedirectToAction(nameof(Index));
                }

                // Crear un nuevo libro de Excel
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("ReporteProveedores");

                // Título del reporte
                worksheet.Cell("B1").Value = "REPORTE DE PROVEEDORES";
                worksheet.Cell("B2").Value = "Generado el:";
                worksheet.Cell("C2").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Encabezados de la tabla
                worksheet.Cell("B4").Value = "ID";
                worksheet.Cell("C4").Value = "Nombre";
                worksheet.Cell("D4").Value = "Contacto";
                worksheet.Cell("E4").Value = "Dirección";

                // Formato de los encabezados
                var headerRange = worksheet.Range("B4:E4");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#4472C4");
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                // Llenar datos de los proveedores
                int row = 5; // Comienza después del encabezado
                foreach (var proveedor in proveedores)
                {
                    worksheet.Cell(row, 2).Value = proveedor.Id;
                    worksheet.Cell(row, 3).Value = proveedor.Nombre;
                    worksheet.Cell(row, 4).Value = proveedor.Contacto;
                    worksheet.Cell(row, 5).Value = proveedor.Direccion;
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
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Proveedores_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
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