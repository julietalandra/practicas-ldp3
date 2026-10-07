using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia04;

public class listadousuariosModel : PageModel
{
    private readonly AbmContext _context;
    public listadousuariosModel(AbmContext context) { _context = context; }
    public List<Usuario> Lista { get; set; } = new();
    public void OnGet() => Lista = _context.Usuarios.AsNoTracking().OrderBy(u => u.Nombre).ToList();

}
