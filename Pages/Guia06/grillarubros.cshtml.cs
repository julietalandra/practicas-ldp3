using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia06;

public class grillarubrosModel : PageModel
{
    private readonly CatalogoContext _context;
    public grillarubrosModel(CatalogoContext context) { _context = context; }
    [BindProperty(SupportsGet = true)] public int Pagina { get; set; } = 1;
    [BindProperty(SupportsGet = true)] public string? Orden { get; set; }
    public const int TamanoPagina = 3;
    public List<Rubro> Filas { get; set; } = new();
    public int TotalRubros { get; set; }
    public int TotalPaginas { get; set; }
    public string? Mensaje { get; set; }
    public void OnGet() => Cargar();

    public IActionResult OnPostModificar(int codigo, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion) || descripcion.Length > 50)
        { Mensaje = "Ingresá una descripción de hasta 50 caracteres"; Cargar(); return Page(); }
        var rubro = _context.Rubros.Find(codigo);
        if (rubro == null) Mensaje = "No existe ese rubro";
        else
        {
            rubro.Descripcion = descripcion;
            Mensaje = _context.SaveChanges() > 0 ? "Se modificó el rubro" : "No cambiaste ningún dato";
        }
        Cargar();
        return Page();
    }
    public IActionResult OnPostEliminar(int codigo)
    {
        using var transaccion = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable);
        var rubro = _context.Rubros.Find(codigo);
        int cantidad = _context.Articulos.Count(a => a.Codigorubro == codigo);
        if (rubro == null) Mensaje = "No existe ese rubro";
        else if (cantidad > 0) Mensaje = $"El rubro tiene {cantidad} artículos cargados, no se puede borrar";
        else
        {
            _context.Rubros.Remove(rubro);
            _context.SaveChanges();
            Mensaje = "Se borró el rubro";
        }
        transaccion.Commit();
        Cargar();
        return Page();
    }
    public string OrdenSiguiente(string columna) => Orden == columna ? columna + "_desc" : columna;
    private void Cargar()
    {
        IQueryable<Rubro> consulta = _context.Rubros.AsNoTracking();
        consulta = Orden switch
        {
            "descripcion" => consulta.OrderBy(r => r.Descripcion).ThenBy(r => r.Codigo),
            "descripcion_desc" => consulta.OrderByDescending(r => r.Descripcion).ThenBy(r => r.Codigo),
            "codigo_desc" => consulta.OrderByDescending(r => r.Codigo),
            _ => consulta.OrderBy(r => r.Codigo)
        };
        TotalRubros = consulta.Count();
        TotalPaginas = Math.Max(1, (int)Math.Ceiling(TotalRubros / (double)TamanoPagina));
        Pagina = Math.Clamp(Pagina, 1, TotalPaginas);
        Filas = consulta.Skip((Pagina - 1) * TamanoPagina).Take(TamanoPagina).ToList();
    }

}
