using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using System.Security.Claims;

namespace miniMarketSolid.Pages.Catalogo
{
    public class IndexModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public IndexModel(ITiendaOnlineService tienda) { _tienda = tienda; }

        public IReadOnlyCollection<miniMarketSolid.Domain.Entities.Producto> Productos { get; private set; } = Array.Empty<miniMarketSolid.Domain.Entities.Producto>();

        public void OnGet()
        {
            Productos = _tienda.ObtenerProductos();
        }

        public IActionResult OnPost(int idProducto, int cantidad)
        {
            var idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (cantidad < 1) cantidad = 1;
            _tienda.AgregarAlCarrito(idCliente, idProducto, cantidad);
            return RedirectToPage();
        }
    }
}
