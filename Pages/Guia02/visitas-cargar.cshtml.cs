using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracticasLDP3.Pages.Guia02
{
    public class CargarModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public CargarModel(IWebHostEnvironment env ) { _env = env; }
        
        [BindProperty]
        public string ? Nombre { get ; set ; }
        [BindProperty]
        public string ? Pais { get ; set ; }
        [BindProperty]
        public string ? Comentarios { get ; set ; }
        public string ? Mensaje { get ; set ; }
        public void OnGet() { 
        
         }
        public IActionResult OnPost ()
        {
            // true = si el archivo no existe se crea; si existe,
            // true = si el archivo no existe se crea; si existe,
            // se abre y se posiciona el puntero al final (append)
            string ruta = Path.Combine(_env.ContentRootPath, "visitas.txt" ); 
            StreamWriter arch = new StreamWriter(ruta, true ); 

            arch.WriteLine( "Nombre: " + Nombre); 
            arch.WriteLine( "<br>" ); 
            arch.WriteLine("Pais: " + Pais); 
            arch.WriteLine("<br>"); 
            arch.WriteLine("Comentarios:<br>"); 
            arch.WriteLine(Comentarios); 
            arch.WriteLine("<br>"); 
            arch.WriteLine("<hr>"); 
            arch.Close(); 
            
            Mensaje ="Datos Registrados";
            return Page();
        }
    }
}
