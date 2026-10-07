using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia06;

public class grillaarticulosModel : PageModel
{
    private readonly CatalogoContext _context;
    public grillaarticulosModel(CatalogoContext context) { _context = context; }
    [BindProperty(SupportsGet = true)] public string? Orden { get; set; }
    [BindProperty(SupportsGet = true)] public int? Seleccionado { get; set; }
    public List<FilaArticulo> Filas { get; set; } = new();
    public string? DatoSeleccionado { get; set; }
    public void OnGet() => Cargar();
    public void OnGetSeleccionar(int codigo) { Seleccionado = codigo; Cargar(); }
    public string OrdenSiguiente(string columna) => Orden == columna ? columna + "_desc" : columna;
    private void Cargar()
    {
        IQueryable<FilaArticulo> consulta = from ar in _context.Articulos
            join ru in _context.Rubros on ar.Codigorubro equals (int?)ru.Codigo
            select new FilaArticulo
            {
                Codigo = ar.Codigo, Descripcion = ar.Descripcion, Precio = ar.Precio,
                Codigorubro = ar.Codigorubro, Rubro = ru.Descripcion
            };
        consulta = Orden switch
        {
            "codigo" => consulta.OrderBy(a => a.Codigo),
            "codigo_desc" => consulta.OrderByDescending(a => a.Codigo),
            "descripcion_desc" => consulta.OrderByDescending(a => a.Descripcion).ThenBy(a => a.Codigo),
            "precio" => consulta.OrderBy(a => a.Precio).ThenBy(a => a.Codigo),
            "precio_desc" => consulta.OrderByDescending(a => a.Precio).ThenBy(a => a.Codigo),
            "rubro" => consulta.OrderBy(a => a.Rubro).ThenBy(a => a.Codigo),
            "rubro_desc" => consulta.OrderByDescending(a => a.Rubro).ThenBy(a => a.Codigo),
            _ => consulta.OrderBy(a => a.Descripcion).ThenBy(a => a.Codigo)
        };
        Filas = consulta.ToList();
        if (Seleccionado.HasValue)
        {
            var articulo = _context.Articulos.Find(Seleccionado.Value);
            DatoSeleccionado = articulo == null ? "No existe ese artículo" : articulo.Descripcion;
        }
    }

}
