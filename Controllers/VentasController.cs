using Microsoft.AspNetCore.Mvc;
using SupermercadoCRUD2.Models.Dto;
using SupermercadoCRUD2.Servicios;

namespace SupermercadoCRUD2.Controllers
{
    public class VentasController : Controller
    {
        private readonly ServicioVentas _servicioVentas;

        public VentasController(ServicioVentas servicioVentas)
        {
            _servicioVentas = servicioVentas;
        }

        // GET: Ventas
        public async Task<IActionResult> Index(string filtroProducto)
        {
            IEnumerable<VentaDto> ventas;

            if (!string.IsNullOrWhiteSpace(filtroProducto))
            {
                ventas = await _servicioVentas.ObtenerVentasPorProductoAsync(filtroProducto);
                ViewBag.FiltroProducto = filtroProducto;
            }
            else
            {
                ventas = await _servicioVentas.ObtenerTodasLasVentasAsync();
            }

            return View(ventas);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VentaDto ventaDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ventaDto);
            }

            var (exito, mensaje) = await _servicioVentas.CrearVentaAsync(ventaDto);

            if (!exito)
            {
                ModelState.AddModelError(string.Empty, mensaje);
                return View(ventaDto);
            }

            TempData["Mensaje"] = mensaje;
            return RedirectToAction(nameof(Index));
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _servicioVentas.ObtenerVentaPorIdAsync(id.Value);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _servicioVentas.EliminarVentaAsync(id);
            
            if (resultado)
            {
                TempData["Mensaje"] = "Venta eliminada correctamente.";
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar la venta.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
