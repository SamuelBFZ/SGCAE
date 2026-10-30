using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Movimientos
{
    internal static class MovimientoAccesoMapper
    {
        public static MovimientoAccesoDTO ToDto(this MovimientoAcceso movimiento)
        {
            return new MovimientoAccesoDTO
            {
                Id = movimiento.Id,
                CodigoCredencial = movimiento.CodigoCredencial,
                CredencialId = movimiento.CredencialId,
                EmpleadoId = movimiento.EmpleadoId,
                PuntoAccesoId = movimiento.PuntoAccesoId,
                PuntoAccesoSolicitado = movimiento.PuntoAccesoSolicitado,
                Tipo = movimiento.Tipo,
                Resultado = movimiento.Resultado,
                Motivo = movimiento.Motivo,
                OcurridoEn = movimiento.OcurridoEn
            };
        }
    }
}
