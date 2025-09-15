using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Infrastructure.Persistence
{
    public class AppDbContext
    {
        private readonly string rutaArchivo;
        private readonly JsonSerializerOptions opciones =
            new JsonSerializerOptions { WriteIndented = true };

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
            VerificarArchivo();
            Cargar();
        }

        private void VerificarArchivo()
        {
            string directorio = Path.GetDirectoryName(rutaArchivo);
            if (string.IsNullOrWhiteSpace(directorio))
            {
                directorio = ".";
            }

            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            if (!File.Exists(rutaArchivo))
            {
                var snapshot = new Snapshot
                {
                    Clientes = new List<Cliente>(),
                    Productos = new List<Producto>()
                };
                string json = JsonSerializer.Serialize(snapshot, opciones);
                File.WriteAllText(rutaArchivo, json);
            }
        }

        public void Cargar()
        {
            string json = File.ReadAllText(rutaArchivo);
            Snapshot snapshot = JsonSerializer.Deserialize<Snapshot>(json, opciones);

            if (snapshot != null)
            {
                if (snapshot.Clientes != null)
                {
                    Clientes = snapshot.Clientes;
                }
                else
                {
                    Clientes = new List<Cliente>();
                }

                if (snapshot.Productos != null)
                {
                    Productos = snapshot.Productos;
                }
                else
                {
                    Productos = new List<Producto>();
                }
            }
            else
            {
                Clientes = new List<Cliente>();
                Productos = new List<Producto>();
            }
        }

        public void Guardar()
        {
            var snapshot = new Snapshot
            {
                Clientes = Clientes,
                Productos = Productos
            };

            string json = JsonSerializer.Serialize(snapshot, opciones);
            File.WriteAllText(rutaArchivo, json);
        }
    }
}
