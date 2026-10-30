using Microsoft.Extensions.DependencyInjection;
using sgcae.Application.Utilities.Mediator;

namespace sgcae.Application
{
    public static class ApplicationServicesRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMediator, SimpleMediator>();
            return services;
        }
    }
}
