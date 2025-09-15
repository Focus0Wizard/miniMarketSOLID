using System;

namespace miniMarketSolid.Domain.Entities
{
    public class Producto
    {
        #region Atributos
        private int id;
        private string nombre;
        private string descripcion;
        private double precio;
        private int stock;
        private string imagenUrl;
        #endregion

        #region Propiedades
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        public string ImagenUrl
        {
            get { return imagenUrl; }
            set { imagenUrl = value; }
        }
        #endregion

        #region Constructores
        public Producto()
        {

        }
        public Producto(int id, string nombre, string descripcion, double precio, int stock, string imagenUrl)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            this.stock = stock;
            this.imagenUrl = imagenUrl;
        }
        #endregion

        #region Métodos
        public void ActualizarPrecio(double nuevoPrecio)
        {
            precio = nuevoPrecio;
        }
        public void ReducirStock(int cantidad)
        {
            stock = stock - cantidad;
        }
        public void AumentarStock(int cantidad)
        {
            stock = stock + cantidad;
        }
        #endregion
    }
}
