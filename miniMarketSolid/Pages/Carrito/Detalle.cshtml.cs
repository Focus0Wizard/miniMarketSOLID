using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace miniMarketSolid.Pages.Carritos
{
    [Authorize(Roles = "Cliente")]
    public class DetalleModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public DetalleModel(ITiendaOnlineService tienda) { _tienda = tienda; }

        public List<ItemCarrito> Items { get; private set; } = new();
        public decimal Subtotal { get; private set; }
        public decimal Total { get; private set; }

        public record DescuentoVM(string Etiqueta, decimal Monto);
        public List<DescuentoVM> Descuentos { get; private set; } = new();

        public void OnGet()
        {
            int idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var carrito = _tienda.ObtenerCarritoDeCliente(idCliente);

            Items = carrito.Items?.ToList() ?? new List<ItemCarrito>();
            Subtotal = (decimal)Items.Sum(i => i.subtotal);

            decimal desc10 = 0, desc20 = 0;

            int unidades = Items.Sum(i => i.Cantidad);
            if (unidades >= 5)
            {
                desc10 = Subtotal * 0.10m;
                Descuentos.Add(new DescuentoVM("Descuento 10% por comprar 5 o más productos", desc10));
            }

            if (Subtotal >= 2000m)
            {
                desc20 = 20m;
                Descuentos.Add(new DescuentoVM("Descuento Bs. 20 por compras desde Bs. 2000", desc20));
            }

            if (carrito.Descuento != null)
            {
                var aplicado = carrito.Descuento.AplicarDescuento(Subtotal);
                var descExtra = Subtotal - aplicado;
                if (descExtra > 0)
                    Descuentos.Add(new DescuentoVM($"Descuento adicional ({carrito.Descuento.GetType().Name})", descExtra));
            }

            Total = Subtotal - Descuentos.Sum(d => d.Monto);
            if (Total < 0) Total = 0;
        }

        public IActionResult OnPostCantidad(int idProducto, int cantidadNueva)
        {
            int idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _tienda.CambiarCantidadItem(idCliente, idProducto, cantidadNueva);
            return RedirectToPage();
        }

        public IActionResult OnPostQuitar(int idProducto)
        {
            int idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _tienda.QuitarDelCarrito(idCliente, idProducto);
            return RedirectToPage();
        }

        public IActionResult OnPostVaciar()
        {
            int idCliente = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _tienda.VaciarCarrito(idCliente);
            return RedirectToPage();
        }
    }
}
