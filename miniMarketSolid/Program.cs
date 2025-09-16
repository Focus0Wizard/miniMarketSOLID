using Microsoft.AspNetCore.Authentication.Cookies;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Application.Services;
using miniMarketSolid.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToPage("/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Account/Logout");
    options.Conventions.AuthorizeFolder("/Clientes", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Productos", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Catalogo", "ClienteOnly");
    options.Conventions.AuthorizeFolder("/Carrito", "ClienteOnly");
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Account/Login";
        o.AccessDeniedPath = "/Account/Login";
        o.ExpireTimeSpan = TimeSpan.FromHours(8);
        o.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
    o.AddPolicy("ClienteOnly", p => p.RequireRole("Cliente"));
    o.AddPolicy("ClienteOrAdmin", p => p.RequireRole("Cliente", "Admin"));
});

var dataPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "db.txt");
builder.Services.AddSingleton<AppDbContext>(_ => new AppDbContext(dataPath));
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ITiendaOnlineService, TiendaOnline>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
