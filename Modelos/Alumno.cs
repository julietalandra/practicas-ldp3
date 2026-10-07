using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticasLDP3.Modelos;

[Table("alumnos")]
public class Alumno
{
    [Key]
    [Column("dni")]
    public string Dni { get; set; } = "";
    [Column("apellidonom")]
    public string? ApellidoNombre { get; set; }
    [Column("provincia")]
    public string? Provincia { get; set; }
}
