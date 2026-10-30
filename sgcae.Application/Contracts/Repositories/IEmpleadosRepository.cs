using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;

namespace sgcae.Application.Contracts.Repositories
{
    public interface IEmpleadosRepository
    {
        Task<Empleado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Empleado?> GetByIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default);

        Task AddAsync(Empleado empleado, CancellationToken cancellationToken = default);

        void Update(Empleado empleado);

        Task<PaginationResponse<Empleado>> ListAsync(
            PaginationRequest pagination,
            EstadoLaboral? estado,
            CancellationToken cancellationToken = default);
    }
}
