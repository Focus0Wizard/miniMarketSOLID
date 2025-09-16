using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using miniMarketSolid.Application.Interfaces;
using miniMarketSolid.Application.Services;
using miniMarketSolid.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Infra TXT/JSON
builder.Services.AddSingleton(new AppDbContext("/home/jhonn/Documentos/Universidad/8vo_Semestre/Arquitectura_Software/miniMarketSOLID/miniMarketSolid/data/db.txt"));

// Repos
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped(typeof(IProductoRepository), sp => new ProductoRepository(sp.GetRequiredService<AppDbContext>()));

// Servicio
builder.Services.AddScoped<ITiendaOnlineService, TiendaOnline>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
