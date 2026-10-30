using Microsoft.EntityFrameworkCore;
using sgcae.Domain.Entities;

namespace sgcae.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<Credencial> Credenciales => Set<Credencial>();
        public DbSet<PuntoAcceso> PuntosAcceso => Set<PuntoAcceso>();
        public DbSet<MovimientoAcceso> MovimientosAcceso => Set<MovimientoAcceso>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
