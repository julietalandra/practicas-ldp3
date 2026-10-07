using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class altaalumnoModel : PageModel
{
    private readonly AbmContext _context;
    public altaalumnoModel(AbmContext context) { _context = context; }
    [BindProperty] public string? Dni { get; set; }
    [BindProperty] public string? ApellidoNombre { get; set; }
    [BindProperty] public string? Provincia { get; set; }
    public string? Mensaje { get; set; }
    public Alumno? Resultado { get; set; }
    public void OnGet() { }
    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Dni)) { Mensaje = "Ingresá dni"; return Page(); }
        if (Dni.Length != 8 || !Dni.All(char.IsDigit)) { Mensaje = "El DNI debe tener 8 dígitos"; return Page(); }
        if (string.IsNullOrWhiteSpace(ApellidoNombre) || ApellidoNombre.Length > 50 || string.IsNullOrWhiteSpace(Provincia) || Provincia.Length > 30)
        { Mensaje = "Completá apellido y nombre (hasta 50 caracteres) y provincia (hasta 30)"; return Page(); }
        if (_context.Alumnos.Any(x => x.Dni == Dni))
        { Mensaje = "Ese registro ya existe"; return Page(); }
        var nuevo = new Alumno { Dni = Dni!, ApellidoNombre = ApellidoNombre, Provincia = Provincia };
        try
        {
            _context.Alumnos.Add(nuevo);
            _context.SaveChanges();
            Mensaje = "Se registró correctamente";
        }
        catch (DbUpdateException)
        { Mensaje = "No se pudo guardar. Verificá los datos y que no exista un registro con esa clave"; }
        return Page();
    }
}
