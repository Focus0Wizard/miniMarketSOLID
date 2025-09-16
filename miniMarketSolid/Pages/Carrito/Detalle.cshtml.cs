using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using System.Security.Claims;
using miniMarketSolid.Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace miniMarketSolid.Pages.Carritos
{
    public class DetalleModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public DetalleModel(ITiendaOnlineService tienda) { _tienda = tienda; }

        public List<ItemCarrito> Items { get; private set; } = new();
        public double Total { get; private set; }

        public void OnGet()
        {
            var idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var carrito = _tienda.ObtenerCarritoDeCliente(idCliente);
            Items = carrito.Items;
            Total = carrito.CalcularTotal();
        }

        public IActionResult OnPostCantidad(int idProducto, int cantidadNueva)
        {
            var idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _tienda.CambiarCantidadItem(idCliente, idProducto, cantidadNueva);
            return RedirectToPage();
        }

        public IActionResult OnPostQuitar(int idProducto)
        {
            var idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _tienda.QuitarDelCarrito(idCliente, idProducto);
            return RedirectToPage();
        }

        public IActionResult OnPostVaciar()
        {
            var idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _tienda.VaciarCarrito(idCliente);
            return RedirectToPage();
        }
    }
}
