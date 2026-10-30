using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Movimientos
{
    public class HistorialAccesoDTO
    {
        public Guid Id { get; init; }
        public TipoMovimiento Tipo { get; init; }
        public ResultadoMovimiento Resultado { get; init; }
        public MotivoRechazo? Motivo { get; init; }
        public string CodigoCredencial { get; init; } = string.Empty;
        public Guid? EmpleadoId { get; init; }
        public Guid? PuntoAccesoId { get; init; }
        public Guid PuntoAccesoSolicitado { get; init; }
        public string? NombrePunto { get; init; }
        public DateTime OcurridoEn { get; init; }
    }
}
