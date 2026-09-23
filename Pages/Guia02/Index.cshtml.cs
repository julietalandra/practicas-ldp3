using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracticasLDP3.Pages.Guia02
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public IndexModel(IWebHostEnvironment env) { _env = env; }
        public string? Valor
        { get; set; }
        public void OnGet()
        {
            string ruta = Path.Combine(_env.ContentRootPath, "contador.txt");
            // File.Exists es un método estático de la clase File:
            // retorna true si el archivo existe en la carpeta indicada

            if (System.IO.File.Exists(ruta))
            {
                // Se lee el valor almacenado
                StreamReader arch1 = new StreamReader(ruta);
                string valor = arch1.ReadToEnd();
                int contador =
                int.Parse(valor); contador++; arch1.Close();
                // Se crea el archivo sin append (false) y se reescribe el valor incrementado
                StreamWriter arch2 = new StreamWriter(ruta);
                arch2.WriteLine(contador.ToString());
                arch2.Close();
                // Se muestra en la página el valor actual
                this.Valor = contador.ToString();
            }
            else
            {
                // Primera visita: se crea el archivo con el valor 1
                StreamWriter arch = new StreamWriter(ruta);
                arch.WriteLine("1");
                arch.Close();
                this.Valor = "1";
            }
        }
    }
}
