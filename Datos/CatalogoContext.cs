using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Modelos;

namespace PracticasLDP3.Datos;

// Contexto separado porque la guía 04 usa administracion_ef y la 05 administracion.
// Estas tablas se crean con Scripts/guia05_catalogo.sql, no con migraciones.
public class CatalogoContext : DbContext
{
    public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }
    public DbSet<Rubro> Rubros => Set<Rubro>();
    public DbSet<Articulo> Articulos => Set<Articulo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rubro>().Property(r => r.Descripcion).HasColumnType("varchar(50)");
        modelBuilder.Entity<Articulo>().Property(a => a.Descripcion).HasColumnType("varchar(50)");
        modelBuilder.Entity<Articulo>().Property(a => a.Precio).HasColumnType("float");
    }
}
