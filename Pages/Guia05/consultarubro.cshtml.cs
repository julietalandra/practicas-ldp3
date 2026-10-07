using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia05;

public class consultarubroModel : PageModel
{
    private readonly IConfiguration _configuration;
    public consultarubroModel(IConfiguration configuration) { _configuration = configuration; }
    private SqlConnection Conexion() => new(_configuration.GetConnectionString("administracion"));
    public string? Mensaje { get; set; }
    [BindProperty] public int? Codigo { get; set; }
    [BindProperty] public string? Descripcion { get; set; }

    public void OnGet() { }
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid || !Codigo.HasValue || Codigo <= 0) { Mensaje = "Ingresá un código válido"; return Page(); }
        using var conexion = Conexion();
        conexion.Open();
        using var comando = new SqlCommand("SELECT descripcion FROM rubros WHERE codigo = @codigo", conexion);
        comando.Parameters.Add("@codigo", SqlDbType.Int).Value = Codigo!.Value;
        using var lector = comando.ExecuteReader();
        if (!lector.Read()) { Mensaje = "No existe ese rubro"; return Page(); }
        Descripcion = lector.IsDBNull(0) ? null : lector.GetString(0);
        ModelState.Clear();
        Mensaje = "Rubro encontrado: " + Descripcion;
        return Page();
    }
}
