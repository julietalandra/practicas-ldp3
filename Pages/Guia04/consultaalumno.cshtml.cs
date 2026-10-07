using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class consultaalumnoModel : PageModel
{
    private readonly AbmContext _context;
    public consultaalumnoModel(AbmContext context) { _context = context; }
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
        Resultado = _context.Alumnos.Find(Dni);
        if (Resultado == null) Mensaje = "No existe un registro con esa clave";
        return Page();
    }
}
