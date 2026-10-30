using Microsoft.EntityFrameworkCore;
using sgcae.Application.Contracts.Repositories;
using sgcae.Domain.Entities;

namespace sgcae.Persistence.Repositories
{
    public class PuntosAccesoRepository : IPuntosAccesoRepository
    {
        private readonly DataContext _context;

        public PuntosAccesoRepository(DataContext context)
        {
            _context = context;
        }

        public Task<PuntoAcceso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.PuntosAcceso
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}
