using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia05;

public class consultaarticulosModel : PageModel
{
    private readonly IConfiguration _configuration;
    public consultaarticulosModel(IConfiguration configuration) { _configuration = configuration; }
    private SqlConnection Conexion() => new(_configuration.GetConnectionString("administracion"));
    public string? Mensaje { get; set; }
    public List<FilaArticulo> Lista { get; set; } = new();
    public bool Consultado { get; set; }
    [BindProperty] public int? Codigo { get; set; }

    public void OnGet() { }
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid || !Codigo.HasValue || Codigo <= 0) { Mensaje = "Ingresá un código válido"; return Page(); }
        using var conexion = Conexion();
        conexion.Open();
        using var comando = new SqlCommand(@"SELECT ar.codigo, ar.descripcion, ar.precio, ar.codigorubro, ru.descripcion AS rubro
            FROM articulos AS ar JOIN rubros AS ru ON ru.codigo = ar.codigorubro WHERE ar.codigo = @codigo ORDER BY ar.descripcion, ar.codigo", conexion);
        comando.Parameters.Add("@codigo", SqlDbType.Int).Value = Codigo!.Value;
        using var lector = comando.ExecuteReader();
        Lista.Clear();
        while (lector.Read()) Lista.Add(new FilaArticulo
        {
            Codigo = lector.GetInt32(0), Descripcion = lector.IsDBNull(1) ? null : lector.GetString(1),
            Precio = lector.IsDBNull(2) ? null : lector.GetDouble(2),
            Codigorubro = lector.IsDBNull(3) ? null : lector.GetInt32(3),
            Rubro = lector.IsDBNull(4) ? null : lector.GetString(4)
        });
        Consultado = true;
        return Page();
    }
}
