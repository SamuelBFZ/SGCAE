using sgcae.Domain.Exceptions;

namespace sgcae.Domain.Entities
{
    public sealed class MovimientoAcceso
    {
        public Guid Id { get; private set; }
        public string CodigoCredencial { get; private set; } = null!;
        public Guid? CredencialId { get; private set; }
        public Guid? EmpleadoId { get; private set; }
        public Guid? PuntoAccesoId { get; private set; }
        public Guid PuntoAccesoSolicitado { get; private set; }
        public TipoMovimiento Tipo { get; private set; }
        public ResultadoMovimiento Resultado { get; private set; }
        public MotivoRechazo? Motivo { get; private set; }
        public DateTime OcurridoEn { get; private set; }

        private MovimientoAcceso()
        {
        }

        public static MovimientoAcceso Registrar(
            string codigoCredencial,
            Guid puntoAccesoSolicitado,
            TipoMovimiento tipo,
            Credencial? credencial,
            Empleado? empleado,
            PuntoAcceso? punto,
            DateTime ocurridoEn)
        {
            if (tipo is not (TipoMovimiento.Entrada or TipoMovimiento.Salida))
            {
                throw new BusinessRuleException("El tipo de movimiento debe ser Entrada o Salida.");
            }

            if (string.IsNullOrWhiteSpace(codigoCredencial))
            {
                throw new BusinessRuleException("El código de la credencial es requerido.");
            }

            if (puntoAccesoSolicitado == Guid.Empty)
            {
                throw new BusinessRuleException("El punto de acceso es requerido.");
            }

            MotivoRechazo? motivo = ResolverMotivo(credencial, empleado, punto);

            return new MovimientoAcceso
            {
                Id = Guid.CreateVersion7(),
                CodigoCredencial = codigoCredencial,
                CredencialId = credencial?.Id,
                EmpleadoId = credencial is null ? null : credencial.EmpleadoId,
                PuntoAccesoId = punto?.Id,
                PuntoAccesoSolicitado = puntoAccesoSolicitado,
                Tipo = tipo,
                Resultado = motivo is null ? ResultadoMovimiento.Autorizado : ResultadoMovimiento.Rechazado,
                Motivo = motivo,
                OcurridoEn = ocurridoEn
            };
        }

        private static MotivoRechazo? ResolverMotivo(Credencial? credencial, Empleado? empleado, PuntoAcceso? punto)
        {
            if (credencial is null)
            {
                return MotivoRechazo.CredencialNoExiste;
            }

            if (!credencial.PuedeUtilizarse())
            {
                return MotivoRechazo.CredencialNoUtilizable;
            }

            if (empleado is null || empleado.EstadoLaboral != EstadoLaboral.Activo)
            {
                return MotivoRechazo.EmpleadoInactivo;
            }

            if (punto is null)
            {
                return MotivoRechazo.PuntoNoExiste;
            }

            if (!punto.Habilitado)
            {
                return MotivoRechazo.PuntoDeshabilitado;
            }

            return null;
        }
    }
}
