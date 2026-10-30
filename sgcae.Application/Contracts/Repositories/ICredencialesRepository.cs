using sgcae.Domain.Entities;

namespace sgcae.Application.Contracts.Repositories
{
    public interface ICredencialesRepository
    {
        Task<Credencial?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Credencial?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Credencial>> ListByEmpleadoAsync(Guid empleadoId, CancellationToken cancellationToken = default);

        Task AddAsync(Credencial credencial, CancellationToken cancellationToken = default);

        void Update(Credencial credencial);
    }
}
