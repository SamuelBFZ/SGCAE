using Microsoft.EntityFrameworkCore;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.UseCases.Movimientos;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;
using sgcae.Persistence.Extensions;

namespace sgcae.Persistence.Repositories
{
    public class MovimientosAccesoRepository : IMovimientosAccesoRepository
    {
        private readonly DataContext _context;

        public MovimientosAccesoRepository(DataContext context)
        {
            _context = context;
        }

        public Task<MovimientoAcceso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.MovimientosAcceso
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public Task AddAsync(MovimientoAcceso movimiento, CancellationToken cancellationToken = default)
        {
            return _context.MovimientosAcceso.AddAsync(movimiento, cancellationToken).AsTask();
        }

        public Task<PaginationResponse<HistorialAccesoDTO>> ListAsync(
            PaginationRequest pagination,
            HistorialAccesosFiltro filtro,
            CancellationToken cancellationToken = default)
        {
            IQueryable<MovimientoAcceso> movimientos = _context.MovimientosAcceso.AsNoTracking();

            if (filtro.EmpleadoId is Guid empleadoId)
            {
                movimientos = movimientos.Where(m => m.EmpleadoId == empleadoId);
            }

            if (filtro.Desde is DateTime desde)
            {
                movimientos = movimientos.Where(m => m.OcurridoEn >= desde);
            }

            if (filtro.Hasta is DateTime hasta)
            {
                movimientos = movimientos.Where(m => m.OcurridoEn <= hasta);
            }

            if (filtro.PuntoAccesoId is Guid puntoAccesoId)
            {
                movimientos = movimientos.Where(m => m.PuntoAccesoId == puntoAccesoId);
            }

            if (filtro.Resultado is ResultadoMovimiento resultado)
            {
                movimientos = movimientos.Where(m => m.Resultado == resultado);
            }

            IQueryable<HistorialAccesoDTO> consulta =
                from movimiento in movimientos
                join punto in _context.PuntosAcceso.AsNoTracking()
                    on movimiento.PuntoAccesoId equals (Guid?)punto.Id into puntos
                from punto in puntos.DefaultIfEmpty()
                orderby movimiento.OcurridoEn descending, movimiento.Id descending
                select new HistorialAccesoDTO
                {
                    Id = movimiento.Id,
                    Tipo = movimiento.Tipo,
                    Resultado = movimiento.Resultado,
                    Motivo = movimiento.Motivo,
                    CodigoCredencial = movimiento.CodigoCredencial,
                    EmpleadoId = movimiento.EmpleadoId,
                    PuntoAccesoId = movimiento.PuntoAccesoId,
                    PuntoAccesoSolicitado = movimiento.PuntoAccesoSolicitado,
                    NombrePunto = punto.Nombre,
                    OcurridoEn = movimiento.OcurridoEn
                };

            return consulta.ToPagedListAsync(pagination, cancellationToken);
        }
    }
}
