using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;

namespace sgcae.Application.UseCases.PuntosAcceso.Queries.ListarPuntosAcceso
{
    public class ListarPuntosAccesoQuery : IRequest<PaginationResponse<PuntoAccesoDTO>>
    {
        public PaginationRequest Pagination { get; set; } = PaginationRequest.Standard();
        public bool? Habilitado { get; set; }
    }
}
