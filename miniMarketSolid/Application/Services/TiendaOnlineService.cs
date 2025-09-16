using System.Collections.Generic;
using System.Linq;
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
        private readonly AppDbContext db;

        public TiendaOnline(IClienteRepository clienteRepository, IProductoRepository productoRepository, AppDbContext db)
        {
            this.clienteRepository = clienteRepository;
            this.productoRepository = productoRepository;
            this.db = db;
        }

        public IReadOnlyCollection<Cliente> ObtenerClientes() => clienteRepository.ObtenerTodos().AsReadOnly();
        public IReadOnlyCollection<Producto> ObtenerProductos() => productoRepository.ObtenerTodos().AsReadOnly();

        public void RegistrarCliente(Cliente c) { clienteRepository.Agregar(c); clienteRepository.GuardarCambios(); }
        public void ActualizarCliente(Cliente c) { clienteRepository.Actualizar(c); clienteRepository.GuardarCambios(); }
        public void EliminarCliente(int id) { clienteRepository.EliminarPorId(id); clienteRepository.GuardarCambios(); }

        public void RegistrarProducto(Producto p) { productoRepository.Agregar(p); productoRepository.GuardarCambios(); }
        public void ActualizarProducto(Producto p) { productoRepository.Actualizar(p); productoRepository.GuardarCambios(); }
        public void EliminarProducto(int id) { productoRepository.EliminarPorId(id); productoRepository.GuardarCambios(); }

        public Carrito ObtenerCarritoDeCliente(int idCliente)
        {
            var cliente = clienteRepository.BuscarPorId(idCliente);
            if (cliente == null) return new Carrito(new Cliente());
            var carrito = db.ObtenerCarrito(idCliente, cliente);
            return carrito;
        }

        public void AgregarAlCarrito(int idCliente, int idProducto, int cantidad)
        {
            var cliente = clienteRepository.BuscarPorId(idCliente);
            var producto = productoRepository.BuscarPorId(idProducto);
            var carrito = db.ObtenerCarrito(idCliente, cliente);
            carrito.AgregarProducto(producto, cantidad);
            db.GuardarCarrito(idCliente, carrito);
        }

        public void CambiarCantidadItem(int idCliente, int idProducto, int cantidadNueva)
        {
            var cliente = clienteRepository.BuscarPorId(idCliente);
            var carrito = db.ObtenerCarrito(idCliente, cliente);
            var item = carrito.Items.FirstOrDefault(i => i.Producto.Id == idProducto);
            if (item == null) return;
            if (cantidadNueva <= 0) { carrito.RemoverProducto(item.Producto); }
            else
            {
                var actual = item.Cantidad;
                if (cantidadNueva > actual) item.IncrementarCantidad(cantidadNueva - actual);
                else if (cantidadNueva < actual) item.ReducirCantidad(actual - cantidadNueva);
            }
            db.GuardarCarrito(idCliente, carrito);
        }

        public void QuitarDelCarrito(int idCliente, int idProducto)
        {
            var cliente = clienteRepository.BuscarPorId(idCliente);
            var carrito = db.ObtenerCarrito(idCliente, cliente);
            var item = carrito.Items.FirstOrDefault(i => i.Producto.Id == idProducto);
            if (item != null) carrito.RemoverProducto(item.Producto);
            db.GuardarCarrito(idCliente, carrito);
        }

        public void VaciarCarrito(int idCliente)
        {
            var cliente = clienteRepository.BuscarPorId(idCliente);
            var carrito = db.ObtenerCarrito(idCliente, cliente);
            foreach (var it in carrito.Items.ToList()) carrito.RemoverProducto(it.Producto);
            db.GuardarCarrito(idCliente, carrito);
        }
    }
}
