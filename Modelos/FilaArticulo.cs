namespace PracticasLDP3.Modelos;

// Resultado del JOIN: la descripción del rubro no se duplica en la tabla artículos.
public class FilaArticulo
{
    public int Codigo { get; set; }
    public string? Descripcion { get; set; }
    public double? Precio { get; set; }
    public int? Codigorubro { get; set; }
    public string? Rubro { get; set; }
}
