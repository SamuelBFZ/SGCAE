using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.PuntosAcceso.Queries.ObtenerPuntoAccesoPorId
{
    public class ObtenerPuntoAccesoPorIdUseCase : IRequestHandler<ObtenerPuntoAccesoPorIdQuery, PuntoAccesoDTO?>
    {
        private readonly IPuntosAccesoRepository _repository;

        public ObtenerPuntoAccesoPorIdUseCase(IPuntosAccesoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PuntoAccesoDTO?> Handle(ObtenerPuntoAccesoPorIdQuery query)
        {
            PuntoAcceso? punto = await _repository.GetByIdAsync(query.Id);
            return punto?.ToDto();
        }
    }
}
