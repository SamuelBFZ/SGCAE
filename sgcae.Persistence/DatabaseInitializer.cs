using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace sgcae.Persistence
{
    public static class DatabaseInitializer
    {
        public static async Task MigrateAsync(IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using IServiceScope scope = services.CreateScope();
            DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
            await context.Database.MigrateAsync(cancellationToken);
        }
    }
}
