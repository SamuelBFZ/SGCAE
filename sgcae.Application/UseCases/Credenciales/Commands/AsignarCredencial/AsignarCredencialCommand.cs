using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Credenciales.Commands.AsignarCredencial
{
    public class AsignarCredencialCommand : IRequest<CredencialDTO?>
    {
        public Guid EmpleadoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }
}
