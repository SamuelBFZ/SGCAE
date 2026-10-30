namespace sgcae.Application.UseCases.PuntosAcceso
{
    public class PuntoAccesoDTO
    {
        public Guid Id { get; init; }
        public required string Nombre { get; init; }
        public bool Habilitado { get; init; }
    }
}
