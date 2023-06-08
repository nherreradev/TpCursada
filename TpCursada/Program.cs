using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using TpCursada.Dominio;
using TpCursada.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Cargar la configuraci�n desde el archivo appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<PW3TiendaContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("EFCoreContext")));

// Agregar el servicio ProductRecommenderIAService al contenedor
builder.Services.AddTransient<ProductRecommenderIAService>();

///
var app = builder.Build();

// Obtener una instancia del servicio ProductRecommenderIAService a trav�s del proveedor de servicios
/**using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var productRecommenderService = serviceProvider.GetRequiredService<ProductRecommenderIAService>();

    // Utilizar el servicio seg�n sea necesario
    //TODO: queda comentado para que no moleste en el front
   productRecommenderService.trainigModelML();
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductRecommender}/{action=index}/{id?}");

app.Run();
