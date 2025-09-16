using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using System.Collections.Generic;

namespace miniMarketSolid.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public IReadOnlyCollection<Producto> ListaProductos { get; private set; }

        public IndexModel(ITiendaOnlineService tienda)
        {
            _tienda = tienda;
            ListaProductos = new List<Producto>().AsReadOnly();
        }

        public void OnGet()
        {
            ListaProductos = _tienda.ObtenerProductos();
        }
    }
}
