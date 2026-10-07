using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class altausuarioModel : PageModel
{
    private readonly AbmContext _context;
    public altausuarioModel(AbmContext context) { _context = context; }
    [BindProperty] public string? Nombre { get; set; }
    [BindProperty] public string? Clave { get; set; }
    [BindProperty] public string? Mail { get; set; }
    public string? Mensaje { get; set; }
    public Usuario? Resultado { get; set; }
    public void OnGet() { }
    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Nombre)) { Mensaje = "Ingresá nombre"; return Page(); }
        
        if (_context.Usuarios.Any(x => x.Nombre == Nombre))
        { Mensaje = "Ese registro ya existe"; return Page(); }
        var nuevo = new Usuario { Nombre = Nombre!, Clave = Clave, Mail = Mail };
        try
        {
            _context.Usuarios.Add(nuevo);
            _context.SaveChanges();
            Mensaje = "Se registró correctamente";
        }
        catch (DbUpdateException)
        { Mensaje = "No se pudo guardar. Verificá los datos y que no exista un registro con esa clave"; }
        return Page();
    }
}
