using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Movimientos.Queries.ObtenerMovimientoPorId
{
    public class ObtenerMovimientoPorIdUseCase : IRequestHandler<ObtenerMovimientoPorIdQuery, MovimientoAccesoDTO?>
    {
        private readonly IMovimientosAccesoRepository _repository;

        public ObtenerMovimientoPorIdUseCase(IMovimientosAccesoRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovimientoAccesoDTO?> Handle(ObtenerMovimientoPorIdQuery query)
        {
            MovimientoAcceso? movimiento = await _repository.GetByIdAsync(query.Id);
            return movimiento?.ToDto();
        }
    }
}
