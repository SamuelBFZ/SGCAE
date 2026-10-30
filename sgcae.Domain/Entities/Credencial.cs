using sgcae.Domain.Exceptions;

namespace sgcae.Domain.Entities
{
    public sealed class Credencial
    {
        public Guid Id { get; private set; }
        public string Codigo { get; private set; } = null!;
        public Guid EmpleadoId { get; private set; }
        public EstadoCredencial Estado { get; private set; }

        private Credencial()
        {
        }

        public static Credencial Asignar(Guid empleadoId, string codigo)
        {
            return new Credencial
            {
                Id = Guid.CreateVersion7(),
                Codigo = Required(codigo),
                EmpleadoId = empleadoId,
                Estado = EstadoCredencial.Vigente
            };
        }

        public bool PuedeUtilizarse()
        {
            return Estado == EstadoCredencial.Vigente;
        }

        public void Bloquear()
        {
            if (Estado == EstadoCredencial.Revocada)
            {
                throw new BusinessRuleException("Una credencial revocada no puede bloquearse.");
            }

            Estado = EstadoCredencial.Bloqueada;
        }

        public void Desbloquear()
        {
            if (Estado != EstadoCredencial.Bloqueada)
            {
                throw new BusinessRuleException("Solo se puede desbloquear una credencial bloqueada.");
            }

            Estado = EstadoCredencial.Vigente;
        }

        public void Revocar()
        {
            if (Estado == EstadoCredencial.Revocada)
            {
                throw new BusinessRuleException("La credencial ya está revocada.");
            }

            Estado = EstadoCredencial.Revocada;
        }

        private static string Required(string? codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new BusinessRuleException("El código de la credencial es requerido.");
            }

            return codigo.Trim();
        }
    }
}
