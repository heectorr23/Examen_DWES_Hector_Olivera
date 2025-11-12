using SupermercadoCRUD2.Data.Repositorios;
using SupermercadoCRUD2.Models.Dto;
using SupermercadoCRUD2.Models.Entity;

namespace SupermercadoCRUD2.Servicios
{
    public class ServicioVentas
    {
        private readonly IVentaRepositorio _ventaRepositorio;

        public ServicioVentas(IVentaRepositorio ventaRepositorio)
        {
            _ventaRepositorio = ventaRepositorio;
        }

        public async Task<IEnumerable<VentaDto>> ObtenerTodasLasVentasAsync()
        {
            var ventas = await _ventaRepositorio.ObtenerTodasAsync();
            return ventas.Select(MapearADto);
        }

        public async Task<IEnumerable<VentaDto>> ObtenerVentasPorProductoAsync(string producto)
        {
            if (string.IsNullOrWhiteSpace(producto))
            {
                return await ObtenerTodasLasVentasAsync();
            }

            var ventas = await _ventaRepositorio.ObtenerPorProductoAsync(producto);
            return ventas.Select(MapearADto);
        }

        public async Task<VentaDto?> ObtenerVentaPorIdAsync(int id)
        {
            var venta = await _ventaRepositorio.ObtenerPorIdAsync(id);
            return venta != null ? MapearADto(venta) : null;
        }

        public async Task<(bool Exito, string Mensaje)> CrearVentaAsync(VentaDto ventaDto)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(ventaDto.Producto))
            {
                return (false, "El nombre del producto es requerido.");
            }

            if (ventaDto.Cantidad <= 0)
            {
                return (false, "La cantidad debe ser mayor que cero.");
            }

            if (ventaDto.Precio <= 0)
            {
                return (false, "El precio debe ser mayor que cero.");
            }

            // Crear la venta
            var venta = new Venta
            {
                Producto = ventaDto.Producto,
                Cantidad = ventaDto.Cantidad,
                Precio = ventaDto.Precio
            };

            await _ventaRepositorio.InsertarAsync(venta);
            return (true, "Venta registrada correctamente.");
        }

        public async Task<bool> EliminarVentaAsync(int id)
        {
            var resultado = await _ventaRepositorio.EliminarAsync(id);
            return resultado > 0;
        }

        private VentaDto MapearADto(Venta venta)
        {
            return new VentaDto
            {
                Id = venta.Id,
                Producto = venta.Producto,
                Cantidad = venta.Cantidad,
                Precio = venta.Precio
            };
        }
    }
}
