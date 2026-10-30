using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Movimientos.Queries.ObtenerMovimientoPorId
{
    public class ObtenerMovimientoPorIdQuery : IRequest<MovimientoAccesoDTO?>
    {
        public Guid Id { get; set; }
    }
}
