using SistemaGestionEnvios.Models;
using SistemaGestionEnvios.Repository.Contrato;
using SistemaGestionEnvios.Repository.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGenericRepository<Distrito>, DistritoRepository>();
builder.Services.AddScoped<IGenericRepository<PaqueteCategoria>, PaqueteCategoriaRepository>();
builder.Services.AddScoped<IGenericRepository<Envio>, EnvioRepository>();
builder.Services.AddScoped<IGenericRepository<EstadoEnvio>, EstadoEnvioRepository>();
builder.Services.AddScoped<IUsuarioRepository<Usuario>, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
