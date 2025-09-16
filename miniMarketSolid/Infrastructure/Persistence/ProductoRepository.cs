using System.Collections.Generic;
using System.Linq;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Infrastructure.Persistence
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;
        public ProductoRepository(AppDbContext context) { _context = context; }

        public List<Producto> ObtenerTodos() => _context.Productos;

        public Producto BuscarPorId(int idProducto) =>
            _context.Productos.FirstOrDefault(p => p.Id == idProducto);

        public void Agregar(Producto producto)
        {
            // Generar Id automáticamente
            int nuevoId = _context.Productos.Any()
                ? _context.Productos.Max(p => p.Id) + 1
                : 1;

            var conId = new Producto(
                nuevoId,
                producto.Nombre,
                producto.Descripcion,
                producto.Precio,
                producto.Stock,
                producto.ImagenUrl
            );

            _context.Productos.Add(conId);
            _context.Guardar();
        }

        public void Actualizar(Producto producto)
        {
            var existente = BuscarPorId(producto.Id);
            if (existente != null)
            {
                existente.Nombre = producto.Nombre;
                existente.Descripcion = producto.Descripcion;
                existente.Precio = producto.Precio;
                existente.Stock = producto.Stock;
                existente.ImagenUrl = producto.ImagenUrl;
                _context.Guardar();
            }
        }

        public void EliminarPorId(int idProducto)
        {
            var existente = BuscarPorId(idProducto);
            if (existente != null)
            {
                _context.Productos.Remove(existente);
                _context.Guardar();
            }
        }

        public void GuardarCambios() => _context.Guardar();
    }
}
