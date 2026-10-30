using sgcae.Domain.Entities;

namespace sgcae.Application.Contracts.Repositories
{
    public sealed class HistorialAccesosFiltro
    {
        public Guid? EmpleadoId { get; init; }
        public DateTime? Desde { get; init; }
        public DateTime? Hasta { get; init; }
        public Guid? PuntoAccesoId { get; init; }
        public ResultadoMovimiento? Resultado { get; init; }
    }
}
