using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Empleados.Queries.ListarEmpleados
{
    public class ListarEmpleadosUseCase : IRequestHandler<ListarEmpleadosQuery, PaginationResponse<EmpleadoDTO>>
    {
        private readonly IEmpleadosRepository _repository;

        public ListarEmpleadosUseCase(IEmpleadosRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginationResponse<EmpleadoDTO>> Handle(ListarEmpleadosQuery query)
        {
            PaginationRequest pagination = query.Pagination;
            PaginationResponse<Empleado> page = await _repository.ListAsync(pagination, query.Estado);

            List<EmpleadoDTO> items = page.Items.Select(empleado => empleado.ToDto()).ToList();
            return PaginationResponse<EmpleadoDTO>.Create(items, page.TotalCount, pagination);
        }
    }
}
