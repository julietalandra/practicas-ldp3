using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticasLDP3.Modelos;

[Table("usuarios")]
public class Usuario
{
    [Key]
    [Column("nombre")]
    public string Nombre { get; set; } = "";
    [Column("clave")]
    public string? Clave { get; set; }
    [Column("mail")]
    public string? Mail { get; set; }
}
