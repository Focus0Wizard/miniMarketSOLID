using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace miniMarketSolid.Pages.Clientes
{
    public class CrearModel : PageModel
    {
        private readonly ITiendaOnlineService _tienda;
        public CrearModel(ITiendaOnlineService tienda) { _tienda = tienda; }

        public class Input
        {
            [Required(ErrorMessage = "El nombre es obligatorio")]
            [StringLength(100)]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "El correo es obligatorio")]
            [EmailAddress(ErrorMessage = "Correo no válido")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "El teléfono es obligatorio")]
            [RegularExpression(@"^\d{8}$", ErrorMessage = "Teléfono debe tener 8 dígitos")]
            public string Telefono { get; set; } = string.Empty;
        }

        [BindProperty] public Input Form { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            string nombreTC = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Form.Nombre.Trim().ToLower());
            string emailNorm = Form.Email.Trim().ToLowerInvariant();
            string telDigits = new string(Form.Telefono.Where(char.IsDigit).ToArray());

            if (telDigits.Length != 8)
            {
                ModelState.AddModelError("Form.Telefono", "Teléfono debe tener 8 dígitos");
                return Page();
            }

            var nuevo = new Cliente(0, nombreTC, emailNorm, int.Parse(telDigits)); // id se asigna en repo
            _tienda.RegistrarCliente(nuevo);
            return RedirectToPage("/Clientes/Index");
        }
    }

}
