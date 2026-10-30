using sgcae.Domain.Entities;

namespace sgcae.Application.Contracts.Repositories
{
    public interface IPuntosAccesoRepository
    {
        Task<PuntoAcceso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
