using System;
using System.Collections.Generic;
using miniMarketSolid.Domain.Entities;
using miniMarketSolid.Domain.Factories;

namespace miniMarketSolid.Application.Services;

public class TiendaOnline
{
    private readonly List<Cliente> clientes = new();
    private readonly List<Producto> productos = new();

    public IReadOnlyCollection<Cliente> Clientes => clientes.AsReadOnly();
    public IReadOnlyCollection<Producto> Productos => productos.AsReadOnly();

    public void RegistrarCliente(Cliente cliente)
    {
        if (cliente == null)
        {
            throw new ArgumentNullException(nameof(cliente));
        }
        clientes.Add(cliente);
    }

    public void RegistrarProducto(Producto producto)
    {
        if (producto == null)
        {
            throw new ArgumentNullException(nameof(producto));
        }
        productos.Add(producto);
    }

    public Carrito CrearCarrito(Cliente clienteNuevo, DescuentoFactory factory)
    {
        if (clienteNuevo == null)
            throw new ArgumentNullException(nameof(clienteNuevo));
        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        var descuento = factory.CrearDescuento();
        var carrito = new Carrito(clienteNuevo, descuento);
        
        clienteNuevo.asignarCarrito(carrito);
        
        return carrito;
    }
}