using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application.UseCases.Credenciales.Queries.ObtenerCredencialPorCodigo
{
    public class ObtenerCredencialPorCodigoQuery : IRequest<CredencialDTO?>
    {
        public string Codigo { get; set; } = string.Empty;
    }
}
