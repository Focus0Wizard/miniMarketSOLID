using System;
using System.Collections.Generic;
using System.Linq;

namespace miniMarketSolid.Domain.Entities;

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
        get => id;
        set => id = value;
    }

    public List<ItemCarrito> Items
    {
        get => items;
        set => items = value ?? throw new ArgumentNullException(nameof(value));
    }
    public Cliente Cliente
    {
        get => cliente;
        set => cliente = value ?? throw new ArgumentNullException(nameof(value));
    }
    public Descuento Descuento
    {
        get => descuento;
        set => descuento = value ?? throw new ArgumentNullException(nameof(value));
    }
    #endregion

    #region Constructor
    public Carrito(Cliente cliente)
    {
        cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
    }
    public Carrito(Cliente cliente, Descuento descuento)
            : this(cliente)
    {
        this.descuento = descuento;
    }
    #endregion

    #region Metodos
    public void AgregarProducto(Producto producto, int cantidad)
    {
        if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor a cero.");

        var existente = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
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
        var existente = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
        if (existente != null)
        {
            Items.Remove(existente);
        }
    }

    public double CalcularTotal()
    {
        double subtotal = Items.Sum(i => i.subtotal);
        if (descuento is null) return subtotal;

        decimal conDescuento = descuento.AplicarDescuento((decimal)subtotal);
        return (double)conDescuento;
    }
    #endregion  
}
