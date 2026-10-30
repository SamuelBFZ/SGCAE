using sgcae.Application.UseCases.Movimientos;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;

namespace sgcae.Application.Contracts.Repositories
{
    public interface IMovimientosAccesoRepository
    {
        Task<MovimientoAcceso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(MovimientoAcceso movimiento, CancellationToken cancellationToken = default);

        Task<PaginationResponse<HistorialAccesoDTO>> ListAsync(
            PaginationRequest pagination,
            HistorialAccesosFiltro filtro,
            CancellationToken cancellationToken = default);
    }
}
