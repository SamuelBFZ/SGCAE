using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Movimientos
{
    public class MovimientoAccesoDTO
    {
        public Guid Id { get; init; }
        public required string CodigoCredencial { get; init; }
        public Guid? CredencialId { get; init; }
        public Guid? EmpleadoId { get; init; }
        public Guid? PuntoAccesoId { get; init; }
        public Guid PuntoAccesoSolicitado { get; init; }
        public TipoMovimiento Tipo { get; init; }
        public ResultadoMovimiento Resultado { get; init; }
        public MotivoRechazo? Motivo { get; init; }
        public DateTime OcurridoEn { get; init; }
    }
}
