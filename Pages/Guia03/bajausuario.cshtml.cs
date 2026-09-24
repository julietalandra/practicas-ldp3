using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace PracticasLDP3.Pages.Guia03
{
    public class bajausuarioModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public bajausuarioModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [BindProperty]
        public string? Nombre { get; set; }
        public string? Mensaje { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string? connectionString =
                _configuration.GetConnectionString("administracion");

            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string sql = @"DELETE FROM usuarios WHERE nombre = @nombre";

            using SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@nombre", Nombre ?? "");

            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas > 0)
            {
                Mensaje = $"Usuario {Nombre} eliminado";
            }
            else
            {
                Mensaje = "No se encontró el usuario";
            }

            return Page();
        }
    }
}
