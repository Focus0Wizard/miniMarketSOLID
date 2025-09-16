using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using System.Collections.Generic;

namespace miniMarketSolid.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public IReadOnlyCollection<Cliente> ListaClientes { get; private set; }

        public IndexModel(ITiendaOnlineService tienda)
        {
            _tienda = tienda;
            ListaClientes = new List<Cliente>().AsReadOnly();
        }

        public void OnGet()
        {
            ListaClientes = _tienda.ObtenerClientes();
        }
    }
}
