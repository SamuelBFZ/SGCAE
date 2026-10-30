using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Credenciales.Queries.ListarCredencialesPorEmpleado
{
    public class ListarCredencialesPorEmpleadoQuery : IRequest<IReadOnlyList<CredencialDTO>?>
    {
        public Guid EmpleadoId { get; set; }
    }
}
