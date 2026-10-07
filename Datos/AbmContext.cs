using Microsoft.EntityFrameworkCore;
using PracticasLDP3.Modelos;

namespace PracticasLDP3.Datos
{
    public class AbmContext : DbContext
    {
        public AbmContext(DbContextOptions<AbmContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
    }
}
