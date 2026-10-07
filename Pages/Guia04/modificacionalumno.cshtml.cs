using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class modificacionalumnoModel : PageModel
{
    private readonly AbmContext _context;
    public modificacionalumnoModel(AbmContext context) { _context = context; }
    [BindProperty] public string? Dni { get; set; }
    [BindProperty] public string? ApellidoNombre { get; set; }
    [BindProperty] public string? Provincia { get; set; }
    public string? Mensaje { get; set; }
    public Alumno? Resultado { get; set; }
    public void OnGet() { }
    public IActionResult OnPostBuscar()
    {
        if (string.IsNullOrWhiteSpace(Dni)) { Mensaje = "Ingresá dni"; return Page(); }
        if (Dni.Length != 8 || !Dni.All(char.IsDigit)) { Mensaje = "El DNI debe tener 8 dígitos"; return Page(); }
        var registro = _context.Alumnos.Find(Dni);
        if (registro == null) { Mensaje = "No existe un registro con esa clave"; return Page(); }
        ApellidoNombre = registro.ApellidoNombre;
        Provincia = registro.Provincia;
        // Los Tag Helpers priorizan ModelState: se limpia para mostrar lo leído.
        ModelState.Clear();
        Mensaje = "Datos cargados. Editá los campos y guardá";
        return Page();
    }
    public IActionResult OnPostGuardar()
    {
        if (string.IsNullOrWhiteSpace(Dni)) { Mensaje = "Ingresá dni"; return Page(); }
        if (Dni.Length != 8 || !Dni.All(char.IsDigit)) { Mensaje = "El DNI debe tener 8 dígitos"; return Page(); }
        if (string.IsNullOrWhiteSpace(ApellidoNombre) || ApellidoNombre.Length > 50 || string.IsNullOrWhiteSpace(Provincia) || Provincia.Length > 30)
        { Mensaje = "Completá apellido y nombre (hasta 50 caracteres) y provincia (hasta 30)"; return Page(); }
        var registro = _context.Alumnos.Find(Dni);
        if (registro == null) { Mensaje = "No existe un registro con esa clave"; return Page(); }
        registro.ApellidoNombre = ApellidoNombre;
        registro.Provincia = Provincia;
        int cantidad = _context.SaveChanges();
        Mensaje = cantidad > 0 ? "Datos modificados" : "No cambiaste ningún dato";
        return Page();
    }
}
