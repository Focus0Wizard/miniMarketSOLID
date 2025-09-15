using System.Collections.Generic;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Application.Interfaces
{
    public interface IProductoRepository
    {
        List<Producto> ObtenerTodos();
        void Agregar(Producto producto);
        Producto BuscarPorId(int idProducto);
        void GuardarCambios();
    }
}
