using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Empleados.Queries.ListarEmpleados
{
    public class ListarEmpleadosQuery : IRequest<PaginationResponse<EmpleadoDTO>>
    {
        public PaginationRequest Pagination { get; set; } = PaginationRequest.Standard();
        public EstadoLaboral? Estado { get; set; }
    }
}
