using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using miniMarketSolid.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace miniMarketSolid.Pages.Clientes
{
    public class EditarModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public EditarModel(ITiendaOnlineService tienda) { _tienda = tienda; }

        public class Input
        {
            [Required] public int IdCliente { get; set; }
            [Required(ErrorMessage = "El nombre es obligatorio"), StringLength(100)]
            public string Nombre { get; set; } = string.Empty;
            [Required(ErrorMessage = "El correo es obligatorio"), EmailAddress(ErrorMessage = "Correo no válido")]
            public string Email { get; set; } = string.Empty;
            [Required(ErrorMessage = "El teléfono es obligatorio")]
            public string Telefono { get; set; } = string.Empty;
        }

        [BindProperty] public Input Form { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var c = _tienda.ObtenerClientes().FirstOrDefault(x => x.IdCliente == id);
            if (c == null) return RedirectToPage("/Clientes/Index");

            Form = new Input
            {
                IdCliente = c.IdCliente,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono.ToString("D8")
            };
            return Page();
        }

        public IActionResult OnPost()
        {
            string nombre, email; int tel;

            try { nombre = ClienteValidation.NormalizarNombre(Form.Nombre); }
            catch (ArgumentException ex) { ModelState.AddModelError("Form.Nombre", ex.Message); nombre = string.Empty; }

            try { email = ClienteValidation.NormalizarEmail(Form.Email); }
            catch (ArgumentException ex) { ModelState.AddModelError("Form.Email", ex.Message); email = string.Empty; }

            try { tel = ClienteValidation.NormalizarTelefono(Form.Telefono); }
            catch (ArgumentException ex) { ModelState.AddModelError("Form.Telefono", ex.Message); tel = 0; }

            if (!ModelState.IsValid) return Page();

            var actualizado = new Cliente(Form.IdCliente, nombre, email, tel);
            _tienda.ActualizarCliente(actualizado);
            return RedirectToPage("/Clientes/Index");
        }
    }
}
