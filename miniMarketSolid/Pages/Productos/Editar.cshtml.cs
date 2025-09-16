using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace miniMarketSolid.Pages.Productos
{
    public class EditarModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public EditarModel(ITiendaOnlineService tienda) { _tienda = tienda; }

        public class Input
        {
            [Required] public int Id { get; set; }

            [Required(ErrorMessage = "El Nombre es obligatorio")]
            [StringLength(100)] public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "La Descripción es obligatoria")]
            [StringLength(200)] public string Descripcion { get; set; } = string.Empty;

            [Range(0.01, double.MaxValue, ErrorMessage = "El Precio debe ser mayor a 0")]
            public double Precio { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "El Stock no puede ser negativo")]
            public int Stock { get; set; }

            [Required, Url(ErrorMessage = "URL no válida")]
            public string ImagenUrl { get; set; } = string.Empty;
        }

        [BindProperty] public Input Form { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var p = _tienda.ObtenerProductos().FirstOrDefault(x => x.Id == id);
            if (p == null) return RedirectToPage("/Productos/Index");

            Form = new Input
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                ImagenUrl = p.ImagenUrl
            };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var actualizado = new Producto(Form.Id, Form.Nombre, Form.Descripcion,
                                           Form.Precio, Form.Stock, Form.ImagenUrl);

            _tienda.ActualizarProducto(actualizado);
            return RedirectToPage("/Productos/Index");
        }
    }
}
