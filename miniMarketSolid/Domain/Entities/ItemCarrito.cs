namespace miniMarketSolid.Domain.Entities;

public class ItemCarrito
{
    #region Atributos
    private int id;
    private Producto producto;
    private int cantidad;
    private decimal subtotal => producto.Precio * cantidad;
    #endregion
    
    #region Constructores
    public ItemCarrito(int id, Producto producto, int cantidad)
    {
        this.id = id;
        this.producto = producto;
        this.cantidad = cantidad;
    }

    public ItemCarrito()
    {
        
    }
    #endregion
    
    #region Propiedades
    public int Id
    {
        get => id;
        set => id = value;
    }

    public Producto Producto
    {
        get => producto;
        set => producto = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Cantidad
    {
        get => cantidad;
        set => cantidad = value;
    }
    #endregion
    
    #region Metodos
    public void IncrementarCantidad(int nuevaCantidad)
    {
        if (nuevaCantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor a cero.");

        if (cantidad + nuevaCantidad > Producto.Stock)
            throw new InvalidOperationException("No hay suficiente stock disponible.");

        Cantidad += nuevaCantidad;
    }
    
    public void ReducirCantidad(int nuevaCantidad)
    {
        if (nuevaCantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor a cero.");

        if (nuevaCantidad > cantidad)
            throw new InvalidOperationException("No puedes reducir mas de lo que hay en el carrito.");

        cantidad -= nuevaCantidad;
    }
    #endregion
}