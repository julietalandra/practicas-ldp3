using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticasLDP3.Modelos;

[Table("rubros")]
public class Rubro
{
    [Key]
    [Column("codigo")]
    public int Codigo { get; set; }
    [Column("descripcion")]
    public string? Descripcion { get; set; }
}
