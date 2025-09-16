using System.Collections.Generic;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using miniMarketSolid.Domain.Factories;

namespace miniMarketSolid.Application.Services
{
    public class TiendaOnline : ITiendaOnlineService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IProductoRepository _productoRepository;

        public TiendaOnline(IClienteRepository clienteRepository, IProductoRepository productoRepository)
        {
            _clienteRepository = clienteRepository;
            _productoRepository = productoRepository;
        }

        // ----- Consultas -----
        public IReadOnlyCollection<Cliente> ObtenerClientes()
        {
            List<Cliente> datos = _clienteRepository.ObtenerTodos();
            return datos.AsReadOnly();
        }

        public IReadOnlyCollection<Producto> ObtenerProductos()
        {
            List<Producto> datos = _productoRepository.ObtenerTodos();
            return datos.AsReadOnly();
        }

        // ----- Clientes -----
        public void RegistrarCliente(Cliente cliente)
        {
            _clienteRepository.Agregar(cliente);
            _clienteRepository.GuardarCambios();
        }

        public void ActualizarCliente(Cliente cliente)
        {
            _clienteRepository.Actualizar(cliente);
            _clienteRepository.GuardarCambios();
        }

        public void EliminarCliente(int idCliente)
        {
            _clienteRepository.EliminarPorId(idCliente);
            _clienteRepository.GuardarCambios();
        }

        // ----- Productos -----
        public void RegistrarProducto(Producto producto)
        {
            _productoRepository.Agregar(producto);
            _productoRepository.GuardarCambios();
        }

        public void ActualizarProducto(Producto producto)
        {
            _productoRepository.Actualizar(producto);
            _productoRepository.GuardarCambios();
        }

        public void EliminarProducto(int idProducto)
        {
            _productoRepository.EliminarPorId(idProducto);
            _productoRepository.GuardarCambios();
        }

        // ----- Carrito -----
        public Carrito CrearCarrito(Cliente cliente, DescuentoFactory fabricaDescuento)
        {
            IDescuento descuento = fabricaDescuento.CrearDescuento();
            Carrito carrito = new Carrito(cliente, descuento);
            cliente.asignarCarrito(carrito);
            return carrito;
        }
    }
}
