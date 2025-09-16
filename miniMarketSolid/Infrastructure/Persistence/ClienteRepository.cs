using System.Collections.Generic;
using System.Linq;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Infrastructure.Persistence
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _contexto;
        public ClienteRepository(AppDbContext contexto) { _contexto = contexto; }

        public List<Cliente> ObtenerTodos() => _contexto.Clientes;

        public Cliente BuscarPorId(int idCliente)
        {
            return _contexto.Clientes.FirstOrDefault(c => c.IdCliente == idCliente);
        }

        public void Agregar(Cliente cliente)
        {
            int nuevoId = _contexto.Clientes.Count == 0 ? 1 : _contexto.Clientes.Max(c => c.IdCliente) + 1;
            var clienteConId = new Cliente(nuevoId, cliente.Nombre, cliente.Email, cliente.Telefono);
            _contexto.Clientes.Add(clienteConId);
            _contexto.Guardar();
        }


        public void Actualizar(Cliente cliente)
        {
            var existente = BuscarPorId(cliente.IdCliente);
            if (existente != null)
            {
                existente.Nombre = cliente.Nombre;
                existente.Email = cliente.Email;
                existente.Telefono = cliente.Telefono;
                _contexto.Guardar();
            }
        }

        public void EliminarPorId(int idCliente)
        {
            var existente = BuscarPorId(idCliente);
            if (existente != null)
            {
                _contexto.Clientes.Remove(existente);
                _contexto.Guardar();
            }
        }

        public void GuardarCambios() => _contexto.Guardar();
    }
}
