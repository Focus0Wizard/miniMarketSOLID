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
        private IDescuento descuento;
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
        public IDescuento Descuento
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
        public Carrito(Cliente cliente, IDescuento descuento)
            : this(cliente)
        {
            this.descuento = descuento;
        }
        #endregion

        #region Metodos
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

            double totalDescuentos = 0;

            int unidades = Items.Sum(i => i.Cantidad);
            if (unidades >= 5)
            {
                totalDescuentos += subtotal * 0.10;
            }

            if (subtotal >= 2000)
            {
                totalDescuentos += 20.0;
            }

            if (descuento != null)
            {
                decimal aplicado = descuento.AplicarDescuento((decimal)subtotal);
                double descuentoExtra = subtotal - (double)aplicado;
                if (descuentoExtra > 0) totalDescuentos += descuentoExtra;
            }

            double total = subtotal - totalDescuentos;
            return total < 0 ? 0 : total;
        }
        #endregion
    }
}
