using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Persistence.Repositories;
using sgcae.Persistence.UnitOfWorks;

namespace sgcae.Persistence
{
    public static class PersistenceServicesRegistry
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyConnection"));
            });

            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddScoped<IEmpleadosRepository, EmpleadosRepository>();
            services.AddScoped<ICredencialesRepository, CredencialesRepository>();
            services.AddScoped<IPuntosAccesoRepository, PuntosAccesoRepository>();
            services.AddScoped<IMovimientosAccesoRepository, MovimientosAccesoRepository>();

            return services;
        }
    }
}
