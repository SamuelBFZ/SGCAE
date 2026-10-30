using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Empleados.Commands.RegistrarEmpleado
{
    public class RegistrarEmpleadoCommand : IRequest<EmpleadoDTO>
    {
        public string Identificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
    }
}
