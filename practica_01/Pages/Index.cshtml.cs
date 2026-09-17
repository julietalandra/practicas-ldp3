using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    public string? FechaHoy { get; set; }

    [BindProperty]
    public int Contador { get; set; }

    [BindProperty]
    public int X1 { get; set; }

    [BindProperty]
    public int X2 { get; set; }

    [BindProperty] 
    public string? Operacion { get; set; }

    public string? Mensaje1 { get; set; }

    public string? Mensaje2 { get; set; }

    // Recibe los checkboxes marcados (mismo name = mismo grupo)
    [BindProperty]    
    public List<string> Operaciones { get; set; } = new();    
    public string? Mensaje3 { get; set; }



    public void OnGet()
    {
        FechaHoy = DateTime.Today.ToString("dd/MM/yyyy");
    }

    public IActionResult OnPostContador()
    {
        FechaHoy = DateTime.Today.ToString("dd/MM/yyyy");
        Contador++;

        return Page();
    }

    public IActionResult OnPostSuma()
    {
        FechaHoy = DateTime.Today.ToString("dd/MM/yyyy");

        int suma = X1 + X2;
        Mensaje1 = "La suma de los dos valores es: " + suma;

        return Page();
    }

    public IActionResult OnPostSumaoResta()
    {
        if (Operacion == "Sumar") 
            { Mensaje2 = "La suma de los dos valores es: " + (X1 + X2); } 
        else if (Operacion == "Restar") 
            { Mensaje2 = "La diferencia de los dos valores es: " + (X1 - X2); }
        
        return Page();
    }

    public IActionResult OnPostSumaYOResta()
    {
        Mensaje3 = "";        
        // Dos if a la misma altura: ambos checkboxes pueden estar marcados
        if (Operaciones.Contains("Sumar"))        
        {            
            Mensaje3 += "La suma de los dos valores es: " + (X1 + X2) + "<br />";        
        }        
        if (Operaciones.Contains("Restar"))        
        {            
            Mensaje3 += "La diferencia de los dos valores es: " + (X1 - X2);        
        }        
        
        return Page();
    }

    [BindProperty] 
    public List<string> Operaciones2 { get; set; } = new();
    public string? Mensaje4 { get; set; }

    public IActionResult OnPostListBox()
    {
        Mensaje4 = "";
        // Cuatro if independientes: verificamos qué items están seleccionados,        
        // como en la guía original con ListBox1.Items[i].Selected
        if (Operaciones.Contains("Sumar"))
            Mensaje4 += "La suma es: " + (X1 + X2) + "<br />";
        if (Operaciones.Contains("Restar"))   
            Mensaje4 += "La diferencia: " + (X1 - X2) + "<br />";
        if (Operaciones.Contains("Multiplicar"))
            Mensaje4 += "El producto: " + (X1 * X2) + "<br />";
        if (Operaciones.Contains("Dividir"))  
            Mensaje4 += "La división: " + (X1 / X2) + "<br />";

        return Page();
    }
}