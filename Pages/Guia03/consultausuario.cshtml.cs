using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace PracticasLDP3.Pages.Guia03
{
    public class consultausuarioModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public consultausuarioModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class Usuario
        {
            public string? Nombre { get; set; }
            public string? Clave { get; set; }
            public string? Mail { get; set; }
        }
        public List<Usuario> Usuarios { get; set; } = new();
        public string? Mensaje { get; set; }
        [BindProperty]
        public string? Nombre { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostBuscar()
        {
            string? connectionString = _configuration.GetConnectionString("administracion");
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string sql = @"SELECT clave, mail FROM usuarios WHERE nombre = @nombre";
            
            using SqlCommand comando = new SqlCommand(sql, conexion);
            
            comando.Parameters.AddWithValue("@nombre", Nombre ?? "");

            using SqlDataReader reader = comando.ExecuteReader();

            if (reader.Read())
            {
                Usuario usuario = new Usuario
                {
                    Nombre = Nombre,
                    Clave = reader["clave"].ToString(),
                    Mail = reader["mail"].ToString()
                };

                Usuarios.Add(usuario);

                Mensaje = "Usuario encontrado";
            }
            else
            {
                Mensaje = "No se encontró el usuario";
            }

            return Page();

        }

        public IActionResult OnPostMostrarTodos()
        {
            string? connectionString =
                _configuration.GetConnectionString("administracion");

            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string sql = @"SELECT nombre, clave, mail FROM usuarios";

            using SqlCommand comando = new SqlCommand(sql, conexion);

            using SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Usuario usuario = new Usuario
                {
                    Nombre = reader["nombre"].ToString(),
                    Clave = reader["clave"].ToString(),
                    Mail = reader["mail"].ToString()
                };

                Usuarios.Add(usuario);
            }

            return Page();
        }

    }
}
