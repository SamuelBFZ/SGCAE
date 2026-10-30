using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.PuntosAcceso.Commands.RegistrarPuntoAcceso
{
    public class RegistrarPuntoAccesoCommand : IRequest<PuntoAccesoDTO>
    {
        public string Nombre { get; set; } = string.Empty;
    }
}
