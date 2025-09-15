using System;

namespace miniMarketSolid.Domain.Entities
{
    public class ItemCarrito
    {
        #region Atributos
        private int id;
        public Producto producto;
        private int cantidad;
        #endregion

        #region Propiedades
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        public double subtotal
        {
            get
            {
                double valor = producto.Precio * cantidad;
                return valor;
            }
        }
        #endregion

        #region Constructores
        public ItemCarrito()
        {

        }
        public ItemCarrito(Producto producto, int cantidad)
        {
            this.producto = producto;
            this.cantidad = cantidad;
        }
        public ItemCarrito(int id, Producto producto, int cantidad)
        {
            this.id = id;
            this.producto = producto;
            this.cantidad = cantidad;
        }
        #endregion

        #region Métodos
        public void IncrementarCantidad(int nuevaCantidad)
        {
            cantidad = cantidad + nuevaCantidad;
        }

        public void ReducirCantidad(int nuevaCantidad)
        {
            cantidad = cantidad - nuevaCantidad;
        }
        #endregion
    }
}
