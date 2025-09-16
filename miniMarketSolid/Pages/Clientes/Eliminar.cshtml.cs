using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using System.Linq;

namespace miniMarketSolid.Pages.Clientes
{
    public class EliminarModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public Cliente Cliente { get; private set; }

        public EliminarModel(ITiendaOnlineService tienda) { _tienda = tienda; }

        public IActionResult OnGet(int id)
        {
            Cliente = _tienda.ObtenerClientes().FirstOrDefault(x => x.IdCliente == id);
            if (Cliente == null) return RedirectToPage("/Clientes/Index");
            return Page();
        }

        public IActionResult OnPostConfirmar(int id)
        {
            _tienda.EliminarCliente(id);
            return RedirectToPage("/Clientes/Index");
        }
    }
}
