using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
namespace PracticasLDP3.Pages.Guia04;

public class operacionesmasivasModel : PageModel
{
    private readonly AbmContext _context;
    public operacionesmasivasModel(AbmContext context) { _context = context; }
    [BindProperty] public string? Nombre { get; set; }
    [BindProperty] public string? ProvinciaOrigen { get; set; }
    [BindProperty] public string? ProvinciaDestino { get; set; }
    public string? Mensaje { get; set; }
    public void OnGet() { }
    public IActionResult OnPostBorrar()
    {
        if (string.IsNullOrWhiteSpace(Nombre)) { Mensaje = "Ingresá un nombre"; return Page(); }
        // Ejecuta SQL directamente: no se llama a SaveChanges después.
        int cantidad = _context.Usuarios.Where(u => u.Nombre == Nombre).ExecuteDelete();
        Mensaje = $"Usuarios borrados: {cantidad}";
        return Page();
    }
    public IActionResult OnPostActualizar()
    {
        if (string.IsNullOrWhiteSpace(ProvinciaOrigen) || string.IsNullOrWhiteSpace(ProvinciaDestino) || ProvinciaDestino.Length > 30)
        { Mensaje = "Ingresá ambas provincias; el destino admite hasta 30 caracteres"; return Page(); }
        int cantidad = _context.Alumnos.Where(a => a.Provincia == ProvinciaOrigen)
            .ExecuteUpdate(s => s.SetProperty(a => a.Provincia, ProvinciaDestino));
        Mensaje = $"Alumnos actualizados: {cantidad}";
        return Page();
    }

}
