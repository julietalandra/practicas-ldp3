using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace PracticasLDP3.Pages.Guia03
{
    public class modificacionusuarioModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public modificacionusuarioModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [BindProperty]
        public string? Nombre { get; set; }

        [BindProperty]
        public string? Clave { get; set; }

        [BindProperty]
        public string? Mail { get; set; }

        public string? Mensaje { get; set; }

        public bool UsuarioEncontrado { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostBuscar()
        {
            string? connectionString =
                _configuration.GetConnectionString("administracion");

            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string sql = @"SELECT clave, mail
                   FROM usuarios
                   WHERE nombre = @nombre";

            using SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@nombre", Nombre ?? "");

            using SqlDataReader reader = comando.ExecuteReader();

            if (reader.Read())
            {
                Clave = reader["clave"].ToString();
                Mail = reader["mail"].ToString();

                UsuarioEncontrado = true;
                Mensaje = "Usuario encontrado";
            }
            else
            {
                UsuarioEncontrado = false;
                Mensaje = "No se encontró el usuario";
            }

            return Page();
        }

        public IActionResult OnPostModificar()
        {
            string? connectionString =
                _configuration.GetConnectionString("administracion");

            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string sql = @"UPDATE usuarios
                        SET clave = @clave,
                            mail = @mail
                        WHERE nombre = @nombre";

            using SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@nombre", Nombre ?? "");
            comando.Parameters.AddWithValue("@clave", Clave ?? "");
            comando.Parameters.AddWithValue("@mail", Mail ?? "");

            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas > 0)
            {
                Mensaje = $"Usuario {Nombre} modificado";
            }
            else
            {
                Mensaje = "No se encontró el usuario";
            }

            return Page();
        }
    }
}
