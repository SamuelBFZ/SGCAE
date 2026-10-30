using Microsoft.EntityFrameworkCore;
using sgcae.Application.Contracts.Repositories;
using sgcae.Domain.Entities;

namespace sgcae.Persistence.Repositories
{
    public class MovimientosAccesoRepository : IMovimientosAccesoRepository
    {
        private readonly DataContext _context;

        public MovimientosAccesoRepository(DataContext context)
        {
            _context = context;
        }

        public Task<MovimientoAcceso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.MovimientosAcceso
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public Task AddAsync(MovimientoAcceso movimiento, CancellationToken cancellationToken = default)
        {
            return _context.MovimientosAcceso.AddAsync(movimiento, cancellationToken).AsTask();
        }
    }
}
