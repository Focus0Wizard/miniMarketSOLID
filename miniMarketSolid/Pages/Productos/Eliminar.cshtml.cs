using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using System.Linq;

namespace miniMarketSolid.Pages.Productos
{
    public class EliminarModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;

        public Producto Producto { get; private set; }

        public EliminarModel(ITiendaOnlineService tienda) => _tienda = tienda;

        public IActionResult OnGet(int id)
        {
            Producto = _tienda.ObtenerProductos().FirstOrDefault(p => p.Id == id);
            if (Producto == null) return RedirectToPage("/Productos/Index");
            return Page();
        }

        public IActionResult OnPostConfirmar(int id)
        {
            _tienda.EliminarProducto(id);
            return RedirectToPage("/Productos/Index");
        }
    }
}
