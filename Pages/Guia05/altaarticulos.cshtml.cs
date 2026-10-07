using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticasLDP3.Datos;
using PracticasLDP3.Modelos;
namespace PracticasLDP3.Pages.Guia05;

public class altaarticulosModel : PageModel
{
    private readonly IConfiguration _configuration;
    public altaarticulosModel(IConfiguration configuration) { _configuration = configuration; }
    private SqlConnection Conexion() => new(_configuration.GetConnectionString("administracion"));
    public string? Mensaje { get; set; }
    [BindProperty] public int? Codigo { get; set; }
    [BindProperty] public string? Descripcion { get; set; }
    [BindProperty] public string? Precio { get; set; }
    [BindProperty] public int? Codigorubro { get; set; }
    public List<SelectListItem> Rubros { get; set; } = new();

    private void CargarRubros()
    {
        using var conexion = Conexion();
        conexion.Open();
        using var comando = new SqlCommand("SELECT codigo, descripcion FROM rubros ORDER BY descripcion, codigo", conexion);
        using var lector = comando.ExecuteReader();
        Rubros.Clear();
        while (lector.Read()) Rubros.Add(new SelectListItem
        {
            Value = lector.GetInt32(0).ToString(),
            Text = lector.IsDBNull(1) ? "(Sin descripción)" : lector.GetString(1)
        });
    }
    public void OnGet() => CargarRubros();
    public IActionResult OnPost()
    {
        CargarRubros();
        if (!ModelState.IsValid) { Mensaje = "Revisá los valores ingresados"; return Page(); }
        if (string.IsNullOrWhiteSpace(Descripcion) || Descripcion.Length > 50)
        { Mensaje = "Ingresá una descripción de hasta 50 caracteres"; return Page(); }
        if (!PrecioFormulario.IntentarLeer(Precio, out double precio))
        { Mensaje = "Ingresá un precio válido, mayor o igual a cero"; return Page(); }
        if (!Codigorubro.HasValue || Codigorubro <= 0)
        { Mensaje = "Seleccioná un rubro"; return Page(); }
        using var conexion = Conexion();
        conexion.Open();
        using var existe = new SqlCommand("SELECT COUNT(*) FROM rubros WHERE codigo = @codigo", conexion);
        existe.Parameters.Add("@codigo", SqlDbType.Int).Value = Codigorubro.Value;
        if (Convert.ToInt32(existe.ExecuteScalar()) == 0)
        { Mensaje = "El rubro no existe"; return Page(); }
        using var comando = new SqlCommand(
            "INSERT INTO articulos(descripcion, precio, codigorubro) VALUES (@descripcion, @precio, @rubro)", conexion);
        comando.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = Descripcion!;
        comando.Parameters.Add("@precio", SqlDbType.Float).Value = precio;
        comando.Parameters.Add("@rubro", SqlDbType.Int).Value = Codigorubro!.Value;
        comando.ExecuteNonQuery();
        Mensaje = "Se registró el artículo";
        return Page();
    }

}
