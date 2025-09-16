using miniMarketSolid.Domain.Entities;
using System.Collections.Generic;

namespace miniMarketSolid.Application.Interfaces
{
    public interface ITiendaOnlineService
    {
        IReadOnlyCollection<Cliente> ObtenerClientes();
        IReadOnlyCollection<Producto> ObtenerProductos();

        void RegistrarCliente(Cliente cliente);
        void ActualizarCliente(Cliente cliente);
        void EliminarCliente(int idCliente);

        void RegistrarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void EliminarProducto(int idProducto);

        Carrito ObtenerCarritoDeCliente(int idCliente);
        void AgregarAlCarrito(int idCliente, int idProducto, int cantidad);
        void CambiarCantidadItem(int idCliente, int idProducto, int cantidadNueva);
        void QuitarDelCarrito(int idCliente, int idProducto);
        void VaciarCarrito(int idCliente);
    }
}
