using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;

namespace sgcae.Application.Contracts.Repositories
{
    public interface IPuntosAccesoRepository
    {
        Task<PuntoAcceso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PuntoAcceso?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default);

        Task AddAsync(PuntoAcceso punto, CancellationToken cancellationToken = default);

        void Update(PuntoAcceso punto);

        Task<PaginationResponse<PuntoAcceso>> ListAsync(
            PaginationRequest pagination,
            bool? habilitado,
            CancellationToken cancellationToken = default);
    }
}
