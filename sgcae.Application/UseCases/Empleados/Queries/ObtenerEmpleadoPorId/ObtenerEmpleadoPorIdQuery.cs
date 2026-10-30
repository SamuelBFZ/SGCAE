using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Empleados.Queries.ObtenerEmpleadoPorId
{
    public class ObtenerEmpleadoPorIdQuery : IRequest<EmpleadoDTO?>
    {
        public Guid Id { get; set; }
    }
}
