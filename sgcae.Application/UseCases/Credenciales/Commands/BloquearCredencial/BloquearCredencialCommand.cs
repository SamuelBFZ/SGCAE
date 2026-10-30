using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Credenciales.Commands.BloquearCredencial
{
    public class BloquearCredencialCommand : IRequest<CredencialDTO?>
    {
        public Guid Id { get; set; }
    }
}
