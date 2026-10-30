using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.PuntosAcceso.Queries.ObtenerPuntoAccesoPorId
{
    public class ObtenerPuntoAccesoPorIdQuery : IRequest<PuntoAccesoDTO?>
    {
        public Guid Id { get; set; }
    }
}
