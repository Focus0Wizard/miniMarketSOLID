using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace miniMarketSolid.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public class DbShape
        {
            public List<ClienteRow> Clientes { get; set; } = new();
            public List<object> Productos { get; set; } = new();
        }
        public class ClienteRow
        {
            public int IdCliente { get; set; }
            public string Nombre { get; set; } = "";
            public string Email { get; set; } = "";
            public int Telefono { get; set; }
        }

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                if (User.IsInRole("Admin")) return RedirectToPage("/Clientes/Index");
                return RedirectToPage("/Productos/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var username = Request.Form["username"].ToString();
            var password = Request.Form["password"].ToString();

            if (username == "admin" && password == "123")
            {
                await SignIn("admin", "Admin", null);
                return RedirectToPage("/Clientes/Index");
            }

            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "data", "db.txt");
            var json = System.IO.File.ReadAllText(jsonPath);
            var db = JsonSerializer.Deserialize<DbShape>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var match = db?.Clientes.FirstOrDefault(c => c.Nombre == username && c.Telefono.ToString() == password);
            if (match != null)
            {
                await SignIn(match.Nombre, "Cliente", match.IdCliente.ToString());
                return RedirectToPage("/Catalogo/Index");
            }

            TempData["ErrorMessage"] = "Usuario o contraseña incorrectos.";
            return RedirectToPage("/Account/Login");
        }

        private async Task SignIn(string nombre, string rol, string? clienteId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nombre),
                new Claim(ClaimTypes.Role, rol)
            };
            if (!string.IsNullOrEmpty(clienteId))
                claims.Add(new Claim(ClaimTypes.NameIdentifier, clienteId));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}