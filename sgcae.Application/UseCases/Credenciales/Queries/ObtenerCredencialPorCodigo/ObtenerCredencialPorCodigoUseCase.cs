using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Credenciales.Queries.ObtenerCredencialPorCodigo
{
    public class ObtenerCredencialPorCodigoUseCase : IRequestHandler<ObtenerCredencialPorCodigoQuery, CredencialDTO?>
    {
        private readonly ICredencialesRepository _repository;

        public ObtenerCredencialPorCodigoUseCase(ICredencialesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CredencialDTO?> Handle(ObtenerCredencialPorCodigoQuery query)
        {
            Credencial? credencial = await _repository.GetByCodigoAsync(query.Codigo);
            return credencial?.ToDto();
        }
    }
}
