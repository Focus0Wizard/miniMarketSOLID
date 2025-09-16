using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Infrastructure.Persistence
{
    public class AppDbContext
    {
        private readonly string rutaArchivo;

        private readonly JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNameCaseInsensitive = true
        };
        private void NormalizarEncodingAUtf8()
        {
            try
            {
                using var sr = new StreamReader(rutaArchivo, new UTF8Encoding(false, true));
                _ = sr.ReadToEnd();
                return;
            }
            catch
            {
                byte[] bytes = File.ReadAllBytes(rutaArchivo);
                string textoLatin1 = Encoding.Latin1.GetString(bytes);
                File.WriteAllText(rutaArchivo, textoLatin1, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            }
        }

        public List<Cliente> Clientes { get; set; }
        public List<Producto> Productos { get; set; }

        private class Snapshot
        {
            public List<Cliente> Clientes { get; set; }
            public List<Producto> Productos { get; set; }
        }

        public AppDbContext(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;
            Clientes = new List<Cliente>();
            Productos = new List<Producto>();
            AsegurarArchivo();
            Cargar();
        }

        private void AsegurarArchivo()
        {
            string directorio = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            if (!File.Exists(rutaArchivo))
            {
                Snapshot snapshotNuevo = new Snapshot
                {
                    Clientes = new List<Cliente>(),
                    Productos = new List<Producto>()
                };

                string jsonNuevo = JsonSerializer.Serialize(snapshotNuevo, opciones);
                File.WriteAllText(rutaArchivo, jsonNuevo, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            }
        }

        public void Cargar()
        {
            NormalizarEncodingAUtf8();
            string json = File.ReadAllText(rutaArchivo, Encoding.UTF8);
            Snapshot snapshotLeido = JsonSerializer.Deserialize<Snapshot>(json, opciones);

            if (snapshotLeido != null)
            {
                if (snapshotLeido.Clientes != null)
                    Clientes = snapshotLeido.Clientes;
                else
                    Clientes = new List<Cliente>();

                if (snapshotLeido.Productos != null)
                    Productos = snapshotLeido.Productos;
                else
                    Productos = new List<Producto>();
            }
        }

        public void Guardar()
        {
            Snapshot snapshot = new Snapshot
            {
                Clientes = Clientes,
                Productos = Productos
            };

            string json = JsonSerializer.Serialize(snapshot, opciones);
            File.WriteAllText(rutaArchivo, json, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        }
    }
}
