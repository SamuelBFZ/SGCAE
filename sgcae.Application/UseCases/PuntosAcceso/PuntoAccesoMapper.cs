using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.PuntosAcceso
{
    internal static class PuntoAccesoMapper
    {
        public static PuntoAccesoDTO ToDto(this PuntoAcceso punto)
        {
            return new PuntoAccesoDTO
            {
                Id = punto.Id,
                Nombre = punto.Nombre,
                Habilitado = punto.Habilitado
            };
        }
    }
}
