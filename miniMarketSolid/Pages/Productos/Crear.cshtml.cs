using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Pages.Productos
{
    public class CrearModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public CrearModel(ITiendaOnlineService tienda) { _tienda = tienda; }

        [BindProperty] public Producto Form { get; set; } = new Producto();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            _tienda.RegistrarProducto(Form);
            return RedirectToPage("/Productos/Index");
        }
    }
}
