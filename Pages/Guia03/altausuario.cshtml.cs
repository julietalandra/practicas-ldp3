using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace PracticasLDP3.Pages.Guia03
{
    public class altausuarioModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public altausuarioModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string? Nombre { get; set; }
        [BindProperty]
        public string? Clave { get; set; }
        [BindProperty]
        public string? Mail { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string? connectionString = _configuration.GetConnectionString("administracion");
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string sql = @"INSERT INTO usuarios (nombre, clave, mail)
               VALUES (@nombre, @clave, @mail)";

            using SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", Nombre ?? "");
            comando.Parameters.AddWithValue("@clave", Clave ?? "");
            comando.Parameters.AddWithValue("@mail", Mail ?? "");

            comando.ExecuteNonQuery();

            return Page();
        }

    }
}
