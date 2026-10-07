using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Modelos;

namespace PracticasLDP3.Datos;

public class AbmContext : DbContext
{
    public AbmContext(DbContextOptions<AbmContext> options) : base(options) { }
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Alumno> Alumnos => Set<Alumno>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Usuario conserva el mapeo inicial para no cambiar tu migración Inicial.
        modelBuilder.Entity<Alumno>().Property(a => a.Dni).HasColumnType("varchar(8)");
        modelBuilder.Entity<Alumno>().Property(a => a.ApellidoNombre).HasColumnType("varchar(50)");
        modelBuilder.Entity<Alumno>().Property(a => a.Provincia).HasColumnType("varchar(30)");
    }
}
