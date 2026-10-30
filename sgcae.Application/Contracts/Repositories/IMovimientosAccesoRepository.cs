using sgcae.Domain.Entities;

namespace sgcae.Application.Contracts.Repositories
{
    public interface IMovimientosAccesoRepository
    {
        Task<MovimientoAcceso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(MovimientoAcceso movimiento, CancellationToken cancellationToken = default);
    }
}
