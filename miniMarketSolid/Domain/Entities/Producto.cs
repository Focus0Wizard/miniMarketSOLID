using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace miniMarketSolid.Domain.Entities
{
    public class Producto
    {
        #region Propiedades
        [JsonInclude]
        public int Id { get; private set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El Nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Descripción es obligatoria")]
        [StringLength(200, ErrorMessage = "La Descripción no puede exceder los 200 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Precio debe ser mayor a 0")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El Stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El Stock no puede ser negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La URL de la imagen es obligatoria")]
        [Url(ErrorMessage = "Formato de URL no válido")]
        public string ImagenUrl { get; set; }
        #endregion

        #region Constructores
        public Producto() { }

        public Producto(int id, string nombre, string descripcion, double precio, int stock, string imagenUrl)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            ImagenUrl = imagenUrl;
        }
        #endregion

        #region Métodos
        public void ActualizarPrecio(double nuevoPrecio)
        {
            Precio = nuevoPrecio;
        }

        public void ReducirStock(int cantidad)
        {
            Stock -= cantidad;
        }

        public void AumentarStock(int cantidad)
        {
            Stock += cantidad;
        }
        #endregion
    }
}
