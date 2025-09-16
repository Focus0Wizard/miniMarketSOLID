using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;

namespace miniMarketSolid.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnPost()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            Console.WriteLine($"User: {username}, Password: {password}");

            if (username == "admin" && password == "123")
            {
                Console.WriteLine("Login admin correcto, redirigiendo...");
                return RedirectToPage("/Clientes/Index"); 
            }

            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "db.txt");
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            var db = JObject.Parse(jsonData);

            var clientes = db["Clientes"];
            foreach (var cliente in clientes)
            {
                string nombre = cliente["Nombre"]?.ToString();
                string telefono = cliente["Telefono"]?.ToString();

                if (username == nombre && password == telefono)
                {
                    int idCliente = (int)cliente["IdCliente"];
                    Console.WriteLine($"Login cliente correcto: {idCliente}");
                    return RedirectToPage("/Clientes/Index", new { id = idCliente });
                }
            }

            TempData["ErrorMessage"] = "Usuario o contraseña incorrectos.";

            return RedirectToPage("/Index");
        }
    }
}