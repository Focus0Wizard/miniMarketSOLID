using System.Collections.Generic;
using miniMarketSolid.Domain.Entities;
using miniMarketSolid.Domain.Factories;

namespace miniMarketSolid.Application.Interfaces
{
    public interface ITiendaOnlineService
    {
        // Consultas
        IReadOnlyCollection<Cliente> ObtenerClientes();
        IReadOnlyCollection<Producto> ObtenerProductos();

        // Clientes
        void RegistrarCliente(Cliente cliente);
        void ActualizarCliente(Cliente cliente);
        void EliminarCliente(int idCliente);

        // Productos
        void RegistrarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void EliminarProducto(int idProducto);

        // Carrito
        Carrito CrearCarrito(Cliente cliente, DescuentoFactory fabricaDescuento);
    }
}
