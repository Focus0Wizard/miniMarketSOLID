using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace miniMarketSolid.Domain.ValueObjects
{
    public static class ClienteValidation
    {
        public static string NormalizarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");

            var limpio = Regex.Replace(nombre.Trim(), @"\s+", " ").ToLower();
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(limpio);
        }

        public static string NormalizarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo es obligatorio");

            var norm = email.Trim().ToLowerInvariant();
            if (!Regex.IsMatch(norm, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Correo no válido");

            return norm;
        }

        public static int NormalizarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono es obligatorio");

            var digits = new string(telefono.Where(char.IsDigit).ToArray());
            if (digits.Length != 8)
                throw new ArgumentException("Teléfono debe tener 8 dígitos");

            return int.Parse(digits);
        }
    }
}
