using System.Collections.Generic;
using miniMarketSolid.Domain.Entities;
using miniMarketSolid.Domain.Factories;

namespace miniMarketSolid.Application.Interfaces
{
    public interface ITiendaOnlineService
    {
        IReadOnlyCollection<Cliente> ObtenerClientes();
        IReadOnlyCollection<Producto> ObtenerProductos();

        void RegistrarCliente(Cliente cliente);
        void RegistrarProducto(Producto producto);
        void EliminarCliente(int idCliente);
        void EliminarProducto(int idProducto);

        Carrito CrearCarrito(Cliente cliente, DescuentoFactory fabricaDescuento);
    }
}
