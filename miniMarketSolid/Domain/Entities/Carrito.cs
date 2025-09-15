using System;
using System.Collections.Generic;
using System.Linq;

namespace miniMarketSolid.Domain.Entities
{
    public class Carrito
    {
        #region Atributos
        private int id;
        private Cliente cliente;
        private List<ItemCarrito> items;
        private Descuento descuento;
        #endregion

        #region Propiedades
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public List<ItemCarrito> Items
        {
            get { return items; }
            set { items = value; }
        }
        public Descuento Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }
        #endregion

        #region Constructores
        public Carrito(Cliente cliente)
        {
            this.cliente = cliente;
            this.items = new List<ItemCarrito>();
        }
        public Carrito(Cliente cliente, Descuento descuento)
            : this(cliente)
        {
            this.descuento = descuento;
        }
        #endregion

        #region Métodos
        public void AgregarProducto(Producto producto, int cantidad)
        {
            ItemCarrito existente = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);

            if (existente != null)
            {
                existente.IncrementarCantidad(cantidad);
            }
            else
            {
                Items.Add(new ItemCarrito(producto, cantidad));
            }
        }
        public void RemoverProducto(Producto producto)
        {
            ItemCarrito existente = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);

            if (existente != null)
            {
                Items.Remove(existente);
            }
        }
        public double CalcularTotal()
        {
            double subtotal = Items.Sum(i => i.subtotal);

            if (descuento == null)
            {
                return subtotal;
            }

            decimal totalConDescuento = descuento.AplicarDescuento((decimal)subtotal);
            return (double)totalConDescuento;
        }
        #endregion
    }
}
