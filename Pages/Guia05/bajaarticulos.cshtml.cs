using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia05;

public class bajaarticulosModel : PageModel
{
    private readonly IConfiguration _configuration;
    public bajaarticulosModel(IConfiguration configuration) { _configuration = configuration; }
    private SqlConnection Conexion() => new(_configuration.GetConnectionString("administracion"));
    public string? Mensaje { get; set; }
    [BindProperty] public int? Codigo { get; set; }
    public void OnGet() { }
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid || !Codigo.HasValue || Codigo <= 0) { Mensaje = "Ingresá un código válido"; return Page(); }
        using var conexion = Conexion();
        conexion.Open();
        using var comando = new SqlCommand("DELETE FROM articulos WHERE codigo = @codigo", conexion);
        comando.Parameters.Add("@codigo", SqlDbType.Int).Value = Codigo.Value;
        int cantidad = comando.ExecuteNonQuery();
        Mensaje = cantidad > 0 ? "Se borró el artículo. Su rubro se conserva" : "No existe ese artículo";
        return Page();
    }

}
