using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticasLDP3.Modelos;

[Table("articulos")]
public class Articulo
{
    [Key]
    [Column("codigo")]
    public int Codigo { get; set; }
    [Column("descripcion")]
    public string? Descripcion { get; set; }
    [Column("precio")]
    public double? Precio { get; set; }
    [Column("codigorubro")]
    public int? Codigorubro { get; set; }
}
