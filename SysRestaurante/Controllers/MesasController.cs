using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using SysRestaurante.BL.DTOs.MesaDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    public class MesasController : Controller
    {
       readonly IMesasBL mesasbl;
        public MesasController(IMesasBL pMesaBL)
        {
            mesasbl = pMesaBL;
        }

        public async Task<IActionResult> Index(MesasBuscarDTO pMesa = null)
        {
            if (pMesa == null)
                pMesa = new MesasBuscarDTO();
            if (pMesa.Take == 0)
                pMesa.Take = 10;
            var paginacion = await mesasbl.BuscarAsync(pMesa);
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View(paginacion.Data);
        }


        public async Task<IActionResult> Mant(int id, ActionsUI pAccion)
        {
            if (pAccion.EsValidAction())
            {
                ViewBag.ActionsUI = pAccion;
                ViewBag.Error = "";
                MesasMantDTO mesaMantDTO = new MesasMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        mesaMantDTO = await mesasbl.ObtenerPorIdAsync(new MesasMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(mesaMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MesasMantDTO pMesa)
        {
            int result = await mesasbl.CreateAsync(pMesa);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MesasMantDTO pMesa)
        {
            int result = await mesasbl.ModificarAsync(pMesa);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MesasMantDTO pMesa)
        {
            int result = await mesasbl.EliminarAsync(pMesa);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(MesasMantDTO pMesa)
        {
            return RedirectToAction(nameof(Mant), new { id = pMesa.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }

    }
}
