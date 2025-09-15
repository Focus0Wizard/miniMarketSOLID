using System;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Application.Services;
using miniMarketSolid.Domain.Entities;
using miniMarketSolid.Domain.Factories;
using miniMarketSolid.Infrastructure.Persistence;

class Program
{
    static void Main()
    {
        // 1) Configuración de la infraestructura
        string rutaArchivo = "data/db.txt";
        AppDbContext contexto = new AppDbContext(rutaArchivo);

        IClienteRepository clienteRepository = new ClienteRepository(contexto);
        IProductoRepository productoRepository = new ProductoRepository(contexto);
        ITiendaOnlineService tienda = new TiendaOnline(clienteRepository, productoRepository);

        // 2) Mostrar estado inicial
        Console.WriteLine("===== ESTADO INICIAL =====");
        MostrarClientes(tienda.ObtenerClientes());
        MostrarProductos(tienda.ObtenerProductos());

        // 3) Registrar cliente y producto
        Cliente nuevoCliente = new Cliente(3, "Carlos Vargas", "carlos@mail.com", 71234567);
        tienda.RegistrarCliente(nuevoCliente);

        Producto nuevoProducto = new Producto(3, "Gorra", "Visera curva", 30.0, 50, "url3");
        tienda.RegistrarProducto(nuevoProducto);

        Console.WriteLine("\n>>> Estado tras registrar:");
        MostrarClientes(tienda.ObtenerClientes());
        MostrarProductos(tienda.ObtenerProductos());

        // 4) Modificar producto
        Producto productoEditable = productoRepository.BuscarPorId(3);
        if (productoEditable != null)
        {
            productoEditable.ActualizarPrecio(35.0);
            productoEditable.AumentarStock(10);
            productoRepository.GuardarCambios();
        }

        // 5) Eliminar un producto existente
        tienda.EliminarProducto(2); // elimina producto con Id = 2 si existe

        Console.WriteLine("\n>>> Estado tras modificar/eliminar:");
        MostrarProductos(tienda.ObtenerProductos());

        // 6) Crear un carrito con descuento
        DescuentoFactory fabricaDescuento = new DescuentoPorcentajeFactory(10m); // 10%
        Carrito carrito = tienda.CrearCarrito(nuevoCliente, fabricaDescuento);

        Producto producto1 = productoRepository.BuscarPorId(1);
        Producto producto3 = productoRepository.BuscarPorId(3);

        if (producto1 != null) carrito.AgregarProducto(producto1, 2);
        if (producto3 != null) carrito.AgregarProducto(producto3, 1);

        Console.WriteLine("\n===== CARRITO =====");
        Console.WriteLine("Cliente: " + carrito.Cliente.Nombre);
        foreach (var item in carrito.Items)
        {
            Console.WriteLine("- " + item.Producto.Nombre + " x" + item.Cantidad + " = " + item.subtotal);
        }
        Console.WriteLine("Total con descuento: " + carrito.CalcularTotal());

        // 7) Eliminar cliente
        tienda.EliminarCliente(1); // elimina cliente con Id = 1 si existe
        Console.WriteLine("\n>>> Estado tras eliminar cliente:");
        MostrarClientes(tienda.ObtenerClientes());

        Console.WriteLine("\nDatos persistidos en: " + rutaArchivo);
    }

    static void MostrarClientes(IReadOnlyCollection<Cliente> clientes)
    {
        Console.WriteLine("Clientes:");
        foreach (var cliente in clientes)
        {
            Console.WriteLine(
                "Id: " + cliente.IdCliente +
                " | Nombre: " + cliente.Nombre +
                " | Email: " + cliente.Email +
                " | Teléfono: " + cliente.Telefono
            );
        }
    }

    static void MostrarProductos(IReadOnlyCollection<Producto> productos)
    {
        Console.WriteLine("Productos:");
        foreach (var producto in productos)
        {
            Console.WriteLine(
                "Id: " + producto.Id +
                " | Nombre: " + producto.Nombre +
                " | Precio: " + producto.Precio +
                " | Stock: " + producto.Stock
            );
        }
    }
}
