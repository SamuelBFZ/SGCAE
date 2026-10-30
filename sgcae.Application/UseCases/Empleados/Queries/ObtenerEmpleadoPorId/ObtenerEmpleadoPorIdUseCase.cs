using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Empleados.Queries.ObtenerEmpleadoPorId
{
    public class ObtenerEmpleadoPorIdUseCase : IRequestHandler<ObtenerEmpleadoPorIdQuery, EmpleadoDTO?>
    {
        private readonly IEmpleadosRepository _repository;

        public ObtenerEmpleadoPorIdUseCase(IEmpleadosRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmpleadoDTO?> Handle(ObtenerEmpleadoPorIdQuery query)
        {
            Empleado? empleado = await _repository.GetByIdAsync(query.Id);
            return empleado?.ToDto();
        }
    }
}
