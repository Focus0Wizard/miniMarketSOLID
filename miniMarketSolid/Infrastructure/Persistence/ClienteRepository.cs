using System.Collections.Generic;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Infrastructure.Persistence
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext contexto;

        public ClienteRepository(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public List<Cliente> ObtenerTodos()
        {
            return contexto.Clientes;
        }

        public void Agregar(Cliente cliente)
        {
            contexto.Clientes.Add(cliente);
        }

        public Cliente BuscarPorId(int idCliente)
        {
            foreach (var cliente in contexto.Clientes)
            {
                if (cliente.IdCliente == idCliente)
                {
                    return cliente;
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
