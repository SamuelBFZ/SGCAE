using Microsoft.Extensions.DependencyInjection;
using sgcae.Application.UseCases.Empleados;
using sgcae.Application.UseCases.Empleados.Commands.ActualizarEmpleado;
using sgcae.Application.UseCases.Empleados.Commands.DesactivarEmpleado;
using sgcae.Application.UseCases.Empleados.Commands.RegistrarEmpleado;
using sgcae.Application.UseCases.Empleados.Queries.ListarEmpleados;
using sgcae.Application.UseCases.Empleados.Queries.ObtenerEmpleadoPorId;
using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;

namespace sgcae.Application
{
    public static class ApplicationServicesRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMediator, SimpleMediator>();

            services.AddScoped<IRequestHandler<RegistrarEmpleadoCommand, EmpleadoDTO>, RegistrarEmpleadoUseCase>();
            services.AddScoped<IRequestHandler<ActualizarEmpleadoCommand, EmpleadoDTO?>, ActualizarEmpleadoUseCase>();
            services.AddScoped<IRequestHandler<DesactivarEmpleadoCommand, EmpleadoDTO?>, DesactivarEmpleadoUseCase>();
            services.AddScoped<IRequestHandler<ObtenerEmpleadoPorIdQuery, EmpleadoDTO?>, ObtenerEmpleadoPorIdUseCase>();
            services.AddScoped<IRequestHandler<ListarEmpleadosQuery, PaginationResponse<EmpleadoDTO>>, ListarEmpleadosUseCase>();

            return services;
        }
    }
}
