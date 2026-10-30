using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.PuntosAcceso.Queries.ListarPuntosAcceso
{
    public class ListarPuntosAccesoUseCase : IRequestHandler<ListarPuntosAccesoQuery, PaginationResponse<PuntoAccesoDTO>>
    {
        private readonly IPuntosAccesoRepository _repository;

        public ListarPuntosAccesoUseCase(IPuntosAccesoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginationResponse<PuntoAccesoDTO>> Handle(ListarPuntosAccesoQuery query)
        {
            PaginationRequest pagination = query.Pagination;
            PaginationResponse<PuntoAcceso> page = await _repository.ListAsync(pagination, query.Habilitado);

            List<PuntoAccesoDTO> items = page.Items.Select(punto => punto.ToDto()).ToList();
            return PaginationResponse<PuntoAccesoDTO>.Create(items, page.TotalCount, pagination);
        }
    }
}
