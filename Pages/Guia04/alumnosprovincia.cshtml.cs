using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class alumnosprovinciaModel : PageModel
{
    private readonly AbmContext _context;
    public alumnosprovinciaModel(AbmContext context) { _context = context; }
    [BindProperty] public string? Provincia { get; set; }
    public List<Alumno> Lista { get; set; } = new();
    public bool Consultado { get; set; }
    public string? Mensaje { get; set; }
    public void OnGet() { }
    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Provincia)) { Mensaje = "Ingresá una provincia"; return Page(); }
        Lista = _context.Alumnos.AsNoTracking().Where(a => a.Provincia != null && a.Provincia.Contains(Provincia))
            .OrderBy(a => a.ApellidoNombre).ThenBy(a => a.Dni).ToList();
        Consultado = true;
        return Page();
    }

}
