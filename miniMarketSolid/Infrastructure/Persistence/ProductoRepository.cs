using System.Collections.Generic;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Infrastructure.Persistence
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext contexto;

        public ProductoRepository(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public List<Producto> ObtenerTodos()
        {
            return contexto.Productos;
        }

        public void Agregar(Producto producto)
        {
            contexto.Productos.Add(producto);
        }

        public Producto BuscarPorId(int idProducto)
        {
            foreach (var producto in contexto.Productos)
            {
                if (producto.Id == idProducto)
                {
                    return producto;
                }
            }
            return null;
        }

        public void GuardarCambios()
        {
            contexto.Guardar();
        }
    }
}
