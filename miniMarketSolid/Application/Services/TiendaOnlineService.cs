using System.Collections.Generic;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using miniMarketSolid.Domain.Factories;
using miniMarketSolid.Infrastructure.Persistence;

namespace miniMarketSolid.Application.Services
{
    public class TiendaOnline : ITiendaOnlineService
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IProductoRepository productoRepository;

        public TiendaOnline(IClienteRepository clienteRepository, IProductoRepository productoRepository)
        {
            this.clienteRepository = clienteRepository;
            this.productoRepository = productoRepository;
        }

        public IReadOnlyCollection<Cliente> ObtenerClientes()
        {
            List<Cliente> listaClientes = clienteRepository.ObtenerTodos();
            return listaClientes.AsReadOnly();
        }

        public IReadOnlyCollection<Producto> ObtenerProductos()
        {
            List<Producto> listaProductos = productoRepository.ObtenerTodos();
            return listaProductos.AsReadOnly();
        }

        public void RegistrarCliente(Cliente cliente)
        {
            clienteRepository.Agregar(cliente);
            clienteRepository.GuardarCambios();
        }

        public void RegistrarProducto(Producto producto)
        {
            productoRepository.Agregar(producto);
            productoRepository.GuardarCambios();
        }

        public void EliminarCliente(int idCliente)
        {
            Cliente clienteEncontrado = clienteRepository.BuscarPorId(idCliente);
            if (clienteEncontrado != null)
            {
                clienteRepository.ObtenerTodos().Remove(clienteEncontrado);
                clienteRepository.GuardarCambios();
            }
        }

        public void EliminarProducto(int idProducto)
        {
            Producto productoEncontrado = productoRepository.BuscarPorId(idProducto);
            if (productoEncontrado != null)
            {
                productoRepository.ObtenerTodos().Remove(productoEncontrado);
                productoRepository.GuardarCambios();
            }
        }

        public Carrito CrearCarrito(Cliente cliente, DescuentoFactory fabricaDescuento)
        {
            IDescuento descuento = fabricaDescuento.CrearDescuento();
            Carrito carrito = new Carrito(cliente, descuento);
            cliente.asignarCarrito(carrito);
            return carrito;
        }
    }
}
