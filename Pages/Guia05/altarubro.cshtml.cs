using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia05;

public class altarubroModel : PageModel
{
    private readonly IConfiguration _configuration;
    public altarubroModel(IConfiguration configuration) { _configuration = configuration; }
    private SqlConnection Conexion() => new(_configuration.GetConnectionString("administracion"));
    public string? Mensaje { get; set; }
    [BindProperty] public int? Codigo { get; set; }
    [BindProperty] public string? Descripcion { get; set; }

    public void OnGet() { }
    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Descripcion) || Descripcion.Length > 50) { Mensaje = "Ingresá una descripción de hasta 50 caracteres"; return Page(); }
        using var conexion = Conexion();
        conexion.Open();
        using var comando = new SqlCommand("INSERT INTO rubros(descripcion) VALUES (@descripcion)", conexion);
        comando.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = Descripcion!;
        comando.ExecuteNonQuery();
        Mensaje = "Se registró el rubro";
        return Page();
    }
}
