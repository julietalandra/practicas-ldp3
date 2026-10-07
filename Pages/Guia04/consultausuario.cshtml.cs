using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class consultausuarioModel : PageModel
{
    private readonly AbmContext _context;
    public consultausuarioModel(AbmContext context) { _context = context; }
    [BindProperty] public string? Nombre { get; set; }
    [BindProperty] public string? Clave { get; set; }
    [BindProperty] public string? Mail { get; set; }
    public string? Mensaje { get; set; }
    public Usuario? Resultado { get; set; }
    public void OnGet() { }
    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Nombre)) { Mensaje = "Ingresá nombre"; return Page(); }
        Resultado = _context.Usuarios.Find(Nombre);
        if (Resultado == null) Mensaje = "No existe un registro con esa clave";
        return Page();
    }
}
