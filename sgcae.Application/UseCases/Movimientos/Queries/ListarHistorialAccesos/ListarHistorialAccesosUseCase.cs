using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;
using sgcae.Domain.Exceptions;

namespace sgcae.Application.UseCases.Movimientos.Queries.ListarHistorialAccesos
{
    public class ListarHistorialAccesosUseCase : IRequestHandler<ListarHistorialAccesosQuery, PaginationResponse<HistorialAccesoDTO>>
    {
        private readonly IMovimientosAccesoRepository _repository;

        public ListarHistorialAccesosUseCase(IMovimientosAccesoRepository repository)
        {
            _repository = repository;
        }

        public Task<PaginationResponse<HistorialAccesoDTO>> Handle(ListarHistorialAccesosQuery query)
        {
            if (query.Desde is DateTime desde && query.Hasta is DateTime hasta && desde > hasta)
            {
                throw new BusinessRuleException("La fecha inicial no puede ser posterior a la fecha final.");
            }

            if (query.Resultado is ResultadoMovimiento resultado
                && resultado is not (ResultadoMovimiento.Autorizado or ResultadoMovimiento.Rechazado))
            {
                throw new BusinessRuleException("El resultado debe ser Autorizado o Rechazado.");
            }

            return _repository.ListAsync(query.Pagination, new HistorialAccesosFiltro
            {
                EmpleadoId = query.EmpleadoId,
                Desde = query.Desde,
                Hasta = query.Hasta,
                PuntoAccesoId = query.PuntoAccesoId,
                Resultado = query.Resultado
            });
        }
    }
}
