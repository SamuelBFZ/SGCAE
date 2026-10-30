using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Credenciales
{
    public class CredencialDTO
    {
        public Guid Id { get; init; }
        public required string Codigo { get; init; }
        public Guid EmpleadoId { get; init; }
        public EstadoCredencial Estado { get; init; }
        public bool PuedeUtilizarse { get; init; }
    }
}
