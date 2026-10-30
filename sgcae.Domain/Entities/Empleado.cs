using sgcae.Domain.Exceptions;

namespace sgcae.Domain.Entities
{
    public sealed class Empleado
    {
        public Guid Id { get; private set; }
        public string Identificacion { get; private set; } = null!;
        public string Nombres { get; private set; } = null!;
        public string Apellidos { get; private set; } = null!;
        public string Correo { get; private set; } = null!;
        public string Cargo { get; private set; } = null!;
        public string Area { get; private set; } = null!;
        public EstadoLaboral EstadoLaboral { get; private set; }

        private Empleado()
        {
        }

        public static Empleado Registrar(
            string identificacion,
            string nombres,
            string apellidos,
            string correo,
            string cargo,
            string area)
        {
            Empleado empleado = new()
            {
                Id = Guid.CreateVersion7(),
                Identificacion = Required(identificacion, "La identificación es requerida."),
                EstadoLaboral = EstadoLaboral.Activo
            };

            empleado.ActualizarDatos(nombres, apellidos, correo, cargo, area);
            return empleado;
        }

        public void ActualizarDatos(string nombres, string apellidos, string correo, string cargo, string area)
        {
            Nombres = Required(nombres, "Los nombres son requeridos.");
            Apellidos = Required(apellidos, "Los apellidos son requeridos.");
            Correo = RequiredEmail(correo);
            Cargo = Required(cargo, "El cargo es requerido.");
            Area = Required(area, "El área es requerida.");
        }

        public void Desactivar()
        {
            EstadoLaboral = EstadoLaboral.Inactivo;
        }

        private static string Required(string? value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BusinessRuleException(message);
            }

            return value.Trim();
        }

        private static string RequiredEmail(string? correo)
        {
            string value = Required(correo, "El correo es requerido.");
            int at = value.IndexOf('@');
            if (at <= 0 || at != value.LastIndexOf('@'))
            {
                throw new BusinessRuleException("El correo no tiene un formato válido.");
            }

            string domain = value[(at + 1)..];
            int dot = domain.IndexOf('.');
            if (dot <= 0 || dot == domain.Length - 1 || domain.Contains(' '))
            {
                throw new BusinessRuleException("El correo no tiene un formato válido.");
            }

            return value;
        }
    }
}
