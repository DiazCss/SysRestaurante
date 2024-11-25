using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System.Security.Claims;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

    public class EmpleadoController : Controller
    {
        readonly IEmpleadoBL empleadoBL;

        public EmpleadoController(IEmpleadoBL pEmpleadoBL)
        {
            empleadoBL = pEmpleadoBL;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        // GET: EmpleadoController
        public async Task<IActionResult> Index(EmpleadoBuscarDTO pEmpleado = null)
        {
            if (pEmpleado == null)
                pEmpleado = new EmpleadoBuscarDTO();
            if (pEmpleado.Take == 0)
                pEmpleado.Take = 10;
            var paginacion = await empleadoBL.BuscarAsync(pEmpleado);
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
                EmpleadoMantDTO empleadoMantDTO = new EmpleadoMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        empleadoMantDTO = await empleadoBL.ObtenerPorIdAsync(new EmpleadoMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(empleadoMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(EmpleadoMantDTO pEmpleado)
{
    try
    {
        int result = await empleadoBL.CreateAsync(pEmpleado);
        TempData["Mensaje"] = "Empleado creado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear el empleado: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(EmpleadoMantDTO pEmpleado)
{
    try
    {
        int result = await empleadoBL.ModificarAsync(pEmpleado);
        TempData["Mensaje"] = "Empleado editado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar el empleado: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(EmpleadoMantDTO pEmpleado)
{
    try
    {
        int result = await empleadoBL.EliminarAsync(pEmpleado);
        TempData["Mensaje"] = "Empleado eliminado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar el empleado: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(EmpleadoMantDTO pEmpleado)
        {
            return RedirectToAction(nameof(Mant), new { id = pEmpleado.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }



        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]
        public async Task<IActionResult> ExportarReporteEmpleados()
        {
            try
            {
                // Obtén los datos del reporte desde la lógica de negocio
                var empleados = await empleadoBL.ObtenerTodosAsync();

                // Verifica que haya datos disponibles
                if (empleados == null || !empleados.Any())
                {
                    TempData["Mensaje"] = "No hay empleados para generar el reporte.";
                    return RedirectToAction("Index");
                }

                // Crear un nuevo libro de Excel
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("ReporteEmpleados");

                // Sección de encabezados
                worksheet.Cell("B1").Value = "REPORTE DE EMPLEADOS";
                worksheet.Cell("B2").Value = "Generado el:";
                worksheet.Cell("C2").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Encabezados de la tabla
                //worksheet.Cell("A4").Value = "ID";
                worksheet.Cell("B4").Value = "Nombre";
                worksheet.Cell("C4").Value = "Apellido";
                worksheet.Cell("D4").Value = "Email";
                worksheet.Cell("E4").Value = "Teléfono";
                worksheet.Cell("F4").Value = "Puesto";
                worksheet.Cell("G4").Value = "Salario";
                worksheet.Cell("H4").Value = "Fecha de Contratación";
                worksheet.Cell("I4").Value = "Estado";

                // Formato de los encabezados
                var headerRange = worksheet.Range("B4:I4");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#BF9000");
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                // Llenar datos de empleados
                int row = 5; // Comienza después del encabezado
                foreach (var empleado in empleados)
                {
                    //worksheet.Cell(row, 1).Value = empleado.Id;
                    worksheet.Cell(row, 2).Value = empleado.Nombre;
                    worksheet.Cell(row, 3).Value = empleado.Apellido;
                    worksheet.Cell(row, 4).Value = empleado.Email;
                    worksheet.Cell(row, 5).Value = empleado.Telefono;
                    worksheet.Cell(row, 6).Value = empleado.Puesto;
                    worksheet.Cell(row, 7).Value = empleado.Salario;
                    worksheet.Cell(row, 8).Value = empleado.FechaContratacion.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 9).Value = empleado.Estado == 0 ? "Activo" : "Inactivo";

                    var dataRange = worksheet.Range($"B{row}:I{row}");
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
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Empleados_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
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
