using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Empleados.Commands.ActualizarEmpleado
{
    public class ActualizarEmpleadoCommand : IRequest<EmpleadoDTO?>
    {
        public Guid Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
    }
}
