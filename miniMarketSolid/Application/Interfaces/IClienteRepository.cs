using System.Collections.Generic;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Application.Interfaces
{
    public interface IClienteRepository
    {
        List<Cliente> ObtenerTodos();
        Cliente BuscarPorId(int idCliente);
        void Agregar(Cliente cliente);
        void Actualizar(Cliente cliente);
        void EliminarPorId(int idCliente);
        void GuardarCambios();
    }
}
