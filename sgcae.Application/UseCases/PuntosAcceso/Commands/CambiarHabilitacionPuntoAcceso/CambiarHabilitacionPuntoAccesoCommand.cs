using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.PuntosAcceso.Commands.CambiarHabilitacionPuntoAcceso
{
    public class CambiarHabilitacionPuntoAccesoCommand : IRequest<PuntoAccesoDTO?>
    {
        public Guid Id { get; set; }
        public bool Habilitado { get; set; }
    }
}
