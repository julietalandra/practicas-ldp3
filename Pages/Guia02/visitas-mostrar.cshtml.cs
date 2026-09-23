using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracticasLDP3.Pages.Guia02
{
    public class MostrarModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public MostrarModel ( IWebHostEnvironment env ) { _env = env; }
        public string ? Contenido { get ; set ; }
        public void OnGet()
        {
            string ruta = Path.Combine(_env.ContentRootPath, "visitas.txt");
            if (System.IO.File.Exists(ruta))
            {
                // Apertura del archivo indicando la ruta donde se encuentra
                StreamReader arch = new StreamReader(ruta);
                // ReadToEnd() retorna el contenido de todo el archivo de texto
                this.Contenido = arch.ReadToEnd();
                // Finalmente se cierra el archivo
                arch.Close();
            }
            else
            {
                this.Contenido = "No hay visitas registradas";
            }   
        }
    }
}
