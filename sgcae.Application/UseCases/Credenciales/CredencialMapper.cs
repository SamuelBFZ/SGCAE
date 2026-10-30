using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Credenciales
{
    internal static class CredencialMapper
    {
        public static CredencialDTO ToDto(this Credencial credencial)
        {
            return new CredencialDTO
            {
                Id = credencial.Id,
                Codigo = credencial.Codigo,
                EmpleadoId = credencial.EmpleadoId,
                Estado = credencial.Estado,
                PuedeUtilizarse = credencial.PuedeUtilizarse()
            };
        }
    }
}
