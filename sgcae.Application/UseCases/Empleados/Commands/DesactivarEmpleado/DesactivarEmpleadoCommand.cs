using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Empleados.Commands.DesactivarEmpleado
{
    public class DesactivarEmpleadoCommand : IRequest<EmpleadoDTO?>
    {
        public Guid Id { get; set; }
    }
}
