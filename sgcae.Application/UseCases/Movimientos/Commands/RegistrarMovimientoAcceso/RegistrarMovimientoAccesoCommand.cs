using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Movimientos.Commands.RegistrarMovimientoAcceso
{
    public class RegistrarMovimientoAccesoCommand : IRequest<MovimientoAccesoDTO>
    {
        public string CodigoCredencial { get; set; } = string.Empty;
        public Guid PuntoAccesoId { get; set; }
        public TipoMovimiento Tipo { get; set; }
    }
}
