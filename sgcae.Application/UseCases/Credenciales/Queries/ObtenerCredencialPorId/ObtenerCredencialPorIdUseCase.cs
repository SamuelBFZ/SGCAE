using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Credenciales.Queries.ObtenerCredencialPorId
{
    public class ObtenerCredencialPorIdUseCase : IRequestHandler<ObtenerCredencialPorIdQuery, CredencialDTO?>
    {
        private readonly ICredencialesRepository _repository;

        public ObtenerCredencialPorIdUseCase(ICredencialesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CredencialDTO?> Handle(ObtenerCredencialPorIdQuery query)
        {
            Credencial? credencial = await _repository.GetByIdAsync(query.Id);
            return credencial?.ToDto();
        }
    }
}
