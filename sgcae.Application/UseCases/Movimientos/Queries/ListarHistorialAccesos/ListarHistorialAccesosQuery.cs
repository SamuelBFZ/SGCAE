using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Movimientos.Queries.ListarHistorialAccesos
{
    public class ListarHistorialAccesosQuery : IRequest<PaginationResponse<HistorialAccesoDTO>>
    {
        public PaginationRequest Pagination { get; set; } = PaginationRequest.Standard();
        public Guid? EmpleadoId { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public Guid? PuntoAccesoId { get; set; }
        public ResultadoMovimiento? Resultado { get; set; }
    }
}
