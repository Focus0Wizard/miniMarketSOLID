using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace miniMarketSolid.Domain.Entities
{
    public class Cliente
    {
        #region Propiedades
        [JsonInclude]
        public int IdCliente { get; private set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El Nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        [Range(60000000, 79999999, ErrorMessage = "Teléfono no válido")]
        public int Telefono { get; set; }

        public Carrito Carrito { get; private set; }
        #endregion

        #region Constructores
        public Cliente() { }

        public Cliente(int idCliente, string nombre, string email, int telefono)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Email = email;
            Telefono = telefono;
        }
        #endregion

        #region Métodos
        public void asignarCarrito(Carrito carritoNuevo)
        {
            Carrito = carritoNuevo;
        }
        #endregion
    }
}
