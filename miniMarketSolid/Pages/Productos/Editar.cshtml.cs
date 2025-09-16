using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using miniMarketSolid.Domain.ValueObjects;
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

            [Required(ErrorMessage = "El nombre es obligatorio")]
            [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "La descripción es obligatoria")]
            [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
            public string Descripcion { get; set; } = string.Empty;

            [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
            public double Precio { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
            public int Stock { get; set; }

            [Required(ErrorMessage = "La URL de imagen es obligatoria")]
            [Url(ErrorMessage = "Formato de URL no válido")]
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
            string nombre, descr, imagen; double precio; int stock;

            try { nombre = ProductoValidation.NormalizarNombre(Form.Nombre); }
            catch (ArgumentException ex) { ModelState.AddModelError("Form.Nombre", ex.Message); nombre = string.Empty; }

            try { descr = ProductoValidation.NormalizarDescripcion(Form.Descripcion); }
            catch (ArgumentException ex) { ModelState.AddModelError("Form.Descripcion", ex.Message); descr = string.Empty; }

            try { precio = ProductoValidation.ValidarPrecio(Form.Precio); }
            catch (ArgumentException ex) { ModelState.AddModelError("Form.Precio", ex.Message); precio = 0; }

            try { stock = ProductoValidation.ValidarStock(Form.Stock); }
            catch (ArgumentException ex) { ModelState.AddModelError("Form.Stock", ex.Message); stock = 0; }

            try { imagen = ProductoValidation.ValidarImagenUrl(Form.ImagenUrl); }
            catch (ArgumentException ex) { ModelState.AddModelError("Form.ImagenUrl", ex.Message); imagen = string.Empty; }

            if (!ModelState.IsValid) return Page();

            var actualizado = new Producto(Form.Id, nombre, descr, precio, stock, imagen);
            _tienda.ActualizarProducto(actualizado);
            return RedirectToPage("/Productos/Index");
        }
    }
}
