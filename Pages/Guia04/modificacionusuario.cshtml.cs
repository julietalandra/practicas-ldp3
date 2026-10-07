using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class modificacionusuarioModel : PageModel
{
    private readonly AbmContext _context;
    public modificacionusuarioModel(AbmContext context) { _context = context; }
    [BindProperty] public string? Nombre { get; set; }
    [BindProperty] public string? Clave { get; set; }
    [BindProperty] public string? Mail { get; set; }
    public string? Mensaje { get; set; }
    public Usuario? Resultado { get; set; }
    public void OnGet() { }
    public IActionResult OnPostBuscar()
    {
        if (string.IsNullOrWhiteSpace(Nombre)) { Mensaje = "Ingresá nombre"; return Page(); }
        var registro = _context.Usuarios.Find(Nombre);
        if (registro == null) { Mensaje = "No existe un registro con esa clave"; return Page(); }
        Clave = registro.Clave;
        Mail = registro.Mail;
        // Los Tag Helpers priorizan ModelState: se limpia para mostrar lo leído.
        ModelState.Clear();
        Mensaje = "Datos cargados. Editá los campos y guardá";
        return Page();
    }
    public IActionResult OnPostGuardar()
    {
        if (string.IsNullOrWhiteSpace(Nombre)) { Mensaje = "Ingresá nombre"; return Page(); }
        
        var registro = _context.Usuarios.Find(Nombre);
        if (registro == null) { Mensaje = "No existe un registro con esa clave"; return Page(); }
        registro.Clave = Clave;
        registro.Mail = Mail;
        int cantidad = _context.SaveChanges();
        Mensaje = cantidad > 0 ? "Datos modificados" : "No cambiaste ningún dato";
        return Page();
    }
}
