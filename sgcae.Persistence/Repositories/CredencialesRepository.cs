using Microsoft.EntityFrameworkCore;
using sgcae.Application.Contracts.Repositories;
using sgcae.Domain.Entities;

namespace sgcae.Persistence.Repositories
{
    public class CredencialesRepository : ICredencialesRepository
    {
        private readonly DataContext _context;

        public CredencialesRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Credencial?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Credenciales.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public Task<Credencial?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default)
        {
            string normalized = codigo.Trim();
            return _context.Credenciales
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Codigo == normalized, cancellationToken);
        }

        public async Task<IReadOnlyList<Credencial>> ListByEmpleadoAsync(Guid empleadoId, CancellationToken cancellationToken = default)
        {
            return await _context.Credenciales
                .AsNoTracking()
                .Where(c => c.EmpleadoId == empleadoId)
                .OrderBy(c => c.Codigo)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(Credencial credencial, CancellationToken cancellationToken = default)
        {
            return _context.Credenciales.AddAsync(credencial, cancellationToken).AsTask();
        }

        public void Update(Credencial credencial)
        {
            _context.Credenciales.Update(credencial);
        }
    }
}
