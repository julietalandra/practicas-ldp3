using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Guía 04: usuarios y alumnos en la base que creamos con migraciones.
builder.Services.AddDbContext<AbmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("administracion_ef")));

// Guía 06: consulta las mismas tablas que la guía 05, en administracion.
builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("administracion")));

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
