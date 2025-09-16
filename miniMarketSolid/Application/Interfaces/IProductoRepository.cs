using System.Collections.Generic;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Application.Interfaces
{
    public interface IProductoRepository
    {
        List<Producto> ObtenerTodos();
        Producto BuscarPorId(int idProducto);
        void Agregar(Producto producto);
        void Actualizar(Producto producto);
        void EliminarPorId(int idProducto);
        void GuardarCambios();
    }

}
