using System.Collections.Generic;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Application.Interfaces
{
    public interface IClienteRepository
    {
        List<Cliente> ObtenerTodos();
        void Agregar(Cliente cliente);
        Cliente BuscarPorId(int idCliente);
        void GuardarCambios();
    }
}
