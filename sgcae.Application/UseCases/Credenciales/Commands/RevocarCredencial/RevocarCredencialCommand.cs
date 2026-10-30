using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Credenciales.Commands.RevocarCredencial
{
    public class RevocarCredencialCommand : IRequest<CredencialDTO?>
    {
        public Guid Id { get; set; }
    }
}
