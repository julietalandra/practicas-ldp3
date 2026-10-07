using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia05;

public class bajarubroModel : PageModel
{
    private readonly IConfiguration _configuration;
    public bajarubroModel(IConfiguration configuration) { _configuration = configuration; }
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
        // La transacción mantiene unidos el control de asociados y la baja.
        using var transaccion = conexion.BeginTransaction(IsolationLevel.Serializable);
        using var contar = new SqlCommand("SELECT COUNT(*) FROM articulos WHERE codigorubro = @codigo", conexion, transaccion);
        contar.Parameters.Add("@codigo", SqlDbType.Int).Value = Codigo!.Value;
        int asociados = Convert.ToInt32(contar.ExecuteScalar());
        if (asociados > 0)
        {
            transaccion.Rollback();
            Mensaje = $"El rubro tiene {asociados} artículos cargados, no se puede borrar";
            return Page();
        }
        using var comando = new SqlCommand("DELETE FROM rubros WHERE codigo = @codigo", conexion, transaccion);
        comando.Parameters.Add("@codigo", SqlDbType.Int).Value = Codigo.Value;
        int cantidad = comando.ExecuteNonQuery();
        transaccion.Commit();
        Mensaje = cantidad > 0 ? "Se borró el rubro" : "No existe ese rubro";
        return Page();
    }
}
