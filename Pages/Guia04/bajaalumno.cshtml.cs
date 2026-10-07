using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class bajaalumnoModel : PageModel
{
    private readonly AbmContext _context;
    public bajaalumnoModel(AbmContext context) { _context = context; }
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
        var registro = _context.Alumnos.Find(Dni);
        if (registro == null) { Mensaje = "No existe un registro con esa clave"; return Page(); }
        _context.Alumnos.Remove(registro);
        _context.SaveChanges();
        Mensaje = "Se borró el registro";
        return Page();
    }
}
