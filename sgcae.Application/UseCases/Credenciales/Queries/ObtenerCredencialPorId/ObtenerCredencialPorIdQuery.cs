using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Credenciales.Queries.ObtenerCredencialPorId
{
    public class ObtenerCredencialPorIdQuery : IRequest<CredencialDTO?>
    {
        public Guid Id { get; set; }
    }
}
