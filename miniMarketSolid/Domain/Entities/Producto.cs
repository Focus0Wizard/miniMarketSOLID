using System;

namespace miniMarketSolid.Domain.Entities;

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
    
    #region Constructores
    public Producto(int id, string nombre, string descripcion, double precio, int stock, string imagenUrl)
    {
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.precio = precio;
        this.stock = stock;
        this.imagenUrl = imagenUrl;
    }

    public Producto() { }
    #endregion
    
    #region Propiedades
    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Nombre
    {
        get => nombre;
        set => nombre = value;
    }

    public string Descripcion
    {
        get => descripcion;
        set => descripcion = value ?? throw new ArgumentNullException(nameof(value));
    }

    public double Precio
    {
        get => precio;
        set => precio = value;
    }

    public int Stock
    {
        get => stock;
        set => stock = value;
    }

    public string ImagenUrl
    {
        get => imagenUrl;
        set => imagenUrl = value ?? throw new ArgumentNullException(nameof(value));
    }
    #endregion
    
    #region Metodos

    public void ActualizarPrecio(double nuevoPrecio)
    {
        if (nuevoPrecio <= 0)
        {
            throw new ArgumentException("El precio debe de ser mayor a cero");
        }
        precio = nuevoPrecio;
    }

    public void ReducirStock(int cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe de ser mayor a cero");
        }

        if (cantidad > stock)
        {
            throw new InvalidOperationException("No hay suficiente stock disponibles");
        }

        stock -= cantidad;
    }
    
    public void AumentarStock(int cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe de ser mayor a cero");
        }

        stock += cantidad;
    }

    #endregion
}