using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Exceptions;

namespace sgcae.Persistence.UnitOfWorks
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public EfCoreUnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception) when (IsUniqueViolation(exception))
            {
                throw new ConflictException("Ya existe un registro con ese valor único.");
            }
        }

        public Task RollbackAsync()
        {
            return Task.CompletedTask;
        }

        private static bool IsUniqueViolation(DbUpdateException exception)
        {
            return exception.InnerException is SqlException sql && sql.Number is 2601 or 2627;
        }
    }
}
