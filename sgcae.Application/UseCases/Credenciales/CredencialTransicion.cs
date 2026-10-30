using sgcae.Application.Exceptions;
using sgcae.Domain.Exceptions;

namespace sgcae.Application.UseCases.Credenciales
{
    internal static class CredencialTransicion
    {
        public static void Ejecutar(Action transicion)
        {
            try
            {
                transicion();
            }
            catch (BusinessRuleException exception)
            {
                throw new ConflictException(exception.Message);
            }
        }
    }
}
