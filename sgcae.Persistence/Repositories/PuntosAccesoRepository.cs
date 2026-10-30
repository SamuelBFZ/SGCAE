using Microsoft.EntityFrameworkCore;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;
using sgcae.Persistence.Extensions;

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
            return _context.PuntosAcceso.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public Task<PuntoAcceso?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default)
        {
            string normalized = nombre.Trim();
            return _context.PuntosAcceso
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Nombre.ToLower() == normalized.ToLower(), cancellationToken);
        }

        public Task AddAsync(PuntoAcceso punto, CancellationToken cancellationToken = default)
        {
            return _context.PuntosAcceso.AddAsync(punto, cancellationToken).AsTask();
        }

        public void Update(PuntoAcceso punto)
        {
            _context.PuntosAcceso.Update(punto);
        }

        public Task<PaginationResponse<PuntoAcceso>> ListAsync(
            PaginationRequest pagination,
            bool? habilitado,
            CancellationToken cancellationToken = default)
        {
            IQueryable<PuntoAcceso> query = _context.PuntosAcceso.AsNoTracking();

            if (habilitado is bool valor)
            {
                query = query.Where(p => p.Habilitado == valor);
            }

            query = query.OrderBy(p => p.Nombre)
                         .ThenBy(p => p.Id);

            return query.ToPagedListAsync(pagination, cancellationToken);
        }
    }
}
