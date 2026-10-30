using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Credenciales.Commands.DesbloquearCredencial
{
    public class DesbloquearCredencialCommand : IRequest<CredencialDTO?>
    {
        public Guid Id { get; set; }
    }
}
