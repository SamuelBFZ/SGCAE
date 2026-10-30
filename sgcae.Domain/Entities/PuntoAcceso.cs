using sgcae.Domain.Exceptions;

namespace sgcae.Domain.Entities
{
    public sealed class PuntoAcceso
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public bool Habilitado { get; private set; }

        private PuntoAcceso()
        {
        }

        public static PuntoAcceso Registrar(string nombre)
        {
            return new PuntoAcceso
            {
                Id = Guid.CreateVersion7(),
                Nombre = Required(nombre),
                Habilitado = true
            };
        }

        public void Habilitar()
        {
            Habilitado = true;
        }

        public void Deshabilitar()
        {
            Habilitado = false;
        }

        private static string Required(string? nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new BusinessRuleException("El nombre del punto de acceso es requerido.");
            }

            return nombre.Trim();
        }
    }
}
