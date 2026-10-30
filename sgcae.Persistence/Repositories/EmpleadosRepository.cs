using Microsoft.EntityFrameworkCore;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;
using sgcae.Persistence.Extensions;

namespace sgcae.Persistence.Repositories
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        private readonly DataContext _context;

        public EmpleadosRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Empleado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Empleados.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Empleado?> GetByIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default)
        {
            string normalized = identificacion.Trim();
            return _context.Empleados
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Identificacion == normalized, cancellationToken);
        }

        public Task AddAsync(Empleado empleado, CancellationToken cancellationToken = default)
        {
            return _context.Empleados.AddAsync(empleado, cancellationToken).AsTask();
        }

        public void Update(Empleado empleado)
        {
            _context.Empleados.Update(empleado);
        }

        public Task<PaginationResponse<Empleado>> ListAsync(
            PaginationRequest pagination,
            EstadoLaboral? estado,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Empleado> query = _context.Empleados.AsNoTracking();

            if (estado is not null)
            {
                query = query.Where(e => e.EstadoLaboral == estado);
            }

            query = query.OrderBy(e => e.Apellidos)
                         .ThenBy(e => e.Nombres)
                         .ThenBy(e => e.Identificacion);

            return query.ToPagedListAsync(pagination, cancellationToken);
        }
    }
}
