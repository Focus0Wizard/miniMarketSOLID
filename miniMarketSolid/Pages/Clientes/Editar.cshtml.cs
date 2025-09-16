using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

            [Required(ErrorMessage = "El nombre es obligatorio")]
            [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "El correo es obligatorio")]
            [EmailAddress(ErrorMessage = "Correo no válido")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "El teléfono es obligatorio")]
            [RegularExpression(@"^\d{8}$", ErrorMessage = "Teléfono debe tener 8 dígitos")]
            public string Telefono { get; set; } = string.Empty;
        }

        [BindProperty] public Input Form { get; set; } = new Input();

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
            if (!ModelState.IsValid) return Page();

            var titulo = CultureInfo.CurrentCulture.TextInfo;
            string nombreTC = titulo.ToTitleCase(Form.Nombre.Trim().ToLower());
            string emailNorm = Form.Email.Trim().ToLowerInvariant();
            string telDigits = new string(Form.Telefono.Where(char.IsDigit).ToArray());
            if (telDigits.Length != 8)
            {
                ModelState.AddModelError("Form.Telefono", "Teléfono debe tener 8 dígitos");
                return Page();
            }

            var actualizado = new Cliente(Form.IdCliente, nombreTC, emailNorm, int.Parse(telDigits));
            _tienda.ActualizarCliente(actualizado);
            return RedirectToPage("/Clientes/Index");
        }
    }
}
