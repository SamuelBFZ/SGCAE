using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Empleados
{
    public class EmpleadoDTO
    {
        public Guid Id { get; init; }
        public required string Identificacion { get; init; }
        public required string Nombres { get; init; }
        public required string Apellidos { get; init; }
        public required string Correo { get; init; }
        public required string Cargo { get; init; }
        public required string Area { get; init; }
        public EstadoLaboral EstadoLaboral { get; init; }
    }
}
