using System.Text.Json;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Infrastructure.Persistence
{
    public class AppDbContext
    {
        private readonly string rutaArchivo;

        public List<Cliente> Clientes { get; private set; } = new();
        public List<Producto> Productos { get; private set; } = new();

        private class CarritoRow { public int IdCliente { get; set; } public List<ItemRow> Items { get; set; } = new(); }
        private class ItemRow { public int IdProducto { get; set; } public int Cantidad { get; set; } }

        private List<CarritoRow> Carritos { get; set; } = new();

        public AppDbContext(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;
            Cargar();
        }

        public void Cargar()
        {
            if (!File.Exists(rutaArchivo))
            {
                Guardar();
                return;
            }
            var json = File.ReadAllText(rutaArchivo);
            if (string.IsNullOrWhiteSpace(json)) { Guardar(); return; }

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            Clientes = root.TryGetProperty("Clientes", out var cEl)
                ? JsonSerializer.Deserialize<List<Cliente>>(cEl.GetRawText()) ?? new List<Cliente>()
                : new List<Cliente>();

            Productos = root.TryGetProperty("Productos", out var pEl)
                ? JsonSerializer.Deserialize<List<Producto>>(pEl.GetRawText()) ?? new List<Producto>()
                : new List<Producto>();

            Carritos = root.TryGetProperty("Carritos", out var kEl)
                ? JsonSerializer.Deserialize<List<CarritoRow>>(kEl.GetRawText()) ?? new List<CarritoRow>()
                : new List<CarritoRow>();
        }

        public void Guardar()
        {
            var payload = new
            {
                Clientes,
                Productos,
                Carritos
            };
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
            var dir = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(rutaArchivo, json);
        }

        public Carrito ObtenerCarrito(int idCliente, Cliente cliente)
        {
            var row = Carritos.FirstOrDefault(c => c.IdCliente == idCliente);
            if (row == null)
            {
                row = new CarritoRow { IdCliente = idCliente };
                Carritos.Add(row);
                Guardar();
            }

            var items = new List<ItemCarrito>();
            foreach (var r in row.Items)
            {
                var prod = Productos.FirstOrDefault(p => p.Id == r.IdProducto);
                if (prod != null) items.Add(new ItemCarrito(0, prod, r.Cantidad));
            }

            var carrito = new Carrito(cliente) { Items = items };
            return carrito;
        }

        public void GuardarCarrito(int idCliente, Carrito carrito)
        {
            var row = Carritos.FirstOrDefault(c => c.IdCliente == idCliente);
            if (row == null)
            {
                row = new CarritoRow { IdCliente = idCliente };
                Carritos.Add(row);
            }
            row.Items = carrito.Items.Select(i => new ItemRow { IdProducto = i.Producto.Id, Cantidad = i.Cantidad }).ToList();
            Guardar();
        }
    }
}
