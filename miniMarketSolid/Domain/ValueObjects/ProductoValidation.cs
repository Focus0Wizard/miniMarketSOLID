using System;
using System.Text.RegularExpressions;

namespace miniMarketSolid.Domain.ValueObjects
{
    public static class ProductoValidation
    {
        public static string NormalizarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");
            return nombre.Trim();
        }

        public static string NormalizarDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción es obligatoria");
            return descripcion.Trim();
        }

        public static double ValidarPrecio(double precio)
        {
            if (precio <= 0) throw new ArgumentException("El precio debe ser mayor a 0");
            return Math.Round(precio, 2);
        }

        public static int ValidarStock(int stock)
        {
            if (stock < 0) throw new ArgumentException("El stock no puede ser negativo");
            return stock;
        }

        public static string ValidarImagenUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("La URL de imagen es obligatoria");

            var u = url.Trim();
            if (!Uri.TryCreate(u, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                throw new ArgumentException("Formato de URL no válido");

            if (!Regex.IsMatch(u, @"\.(png|jpg|jpeg|gif|webp)(\?.*)?$", RegexOptions.IgnoreCase))
                return u;

            return u;
        }
    }
}
