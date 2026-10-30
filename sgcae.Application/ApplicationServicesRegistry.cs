using Microsoft.Extensions.DependencyInjection;
using sgcae.Application.UseCases.Credenciales;
using sgcae.Application.UseCases.Credenciales.Commands.AsignarCredencial;
using sgcae.Application.UseCases.Credenciales.Commands.BloquearCredencial;
using sgcae.Application.UseCases.Credenciales.Commands.DesbloquearCredencial;
using sgcae.Application.UseCases.Credenciales.Commands.RevocarCredencial;
using sgcae.Application.UseCases.Credenciales.Queries.ListarCredencialesPorEmpleado;
using sgcae.Application.UseCases.Credenciales.Queries.ObtenerCredencialPorCodigo;
using sgcae.Application.UseCases.Credenciales.Queries.ObtenerCredencialPorId;
using sgcae.Application.UseCases.Empleados;
using sgcae.Application.UseCases.Movimientos;
using sgcae.Application.UseCases.Movimientos.Commands.RegistrarMovimientoAcceso;
using sgcae.Application.UseCases.Movimientos.Queries.ListarHistorialAccesos;
using sgcae.Application.UseCases.Movimientos.Queries.ObtenerMovimientoPorId;
using sgcae.Application.UseCases.Empleados.Commands.ActualizarEmpleado;
using sgcae.Application.UseCases.Empleados.Commands.DesactivarEmpleado;
using sgcae.Application.UseCases.Empleados.Commands.RegistrarEmpleado;
using sgcae.Application.UseCases.Empleados.Queries.ListarEmpleados;
using sgcae.Application.UseCases.Empleados.Queries.ObtenerEmpleadoPorId;
using sgcae.Application.UseCases.PuntosAcceso;
using sgcae.Application.UseCases.PuntosAcceso.Commands.CambiarHabilitacionPuntoAcceso;
using sgcae.Application.UseCases.PuntosAcceso.Commands.RegistrarPuntoAcceso;
using sgcae.Application.UseCases.PuntosAcceso.Queries.ListarPuntosAcceso;
using sgcae.Application.UseCases.PuntosAcceso.Queries.ObtenerPuntoAccesoPorId;
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

            services.AddScoped<IRequestHandler<AsignarCredencialCommand, CredencialDTO?>, AsignarCredencialUseCase>();
            services.AddScoped<IRequestHandler<BloquearCredencialCommand, CredencialDTO?>, BloquearCredencialUseCase>();
            services.AddScoped<IRequestHandler<DesbloquearCredencialCommand, CredencialDTO?>, DesbloquearCredencialUseCase>();
            services.AddScoped<IRequestHandler<RevocarCredencialCommand, CredencialDTO?>, RevocarCredencialUseCase>();
            services.AddScoped<IRequestHandler<ObtenerCredencialPorIdQuery, CredencialDTO?>, ObtenerCredencialPorIdUseCase>();
            services.AddScoped<IRequestHandler<ObtenerCredencialPorCodigoQuery, CredencialDTO?>, ObtenerCredencialPorCodigoUseCase>();
            services.AddScoped<IRequestHandler<ListarCredencialesPorEmpleadoQuery, IReadOnlyList<CredencialDTO>?>, ListarCredencialesPorEmpleadoUseCase>();

            services.AddScoped<IRequestHandler<RegistrarMovimientoAccesoCommand, MovimientoAccesoDTO>, RegistrarMovimientoAccesoUseCase>();
            services.AddScoped<IRequestHandler<ObtenerMovimientoPorIdQuery, MovimientoAccesoDTO?>, ObtenerMovimientoPorIdUseCase>();
            services.AddScoped<IRequestHandler<ListarHistorialAccesosQuery, PaginationResponse<HistorialAccesoDTO>>, ListarHistorialAccesosUseCase>();

            services.AddScoped<IRequestHandler<RegistrarPuntoAccesoCommand, PuntoAccesoDTO>, RegistrarPuntoAccesoUseCase>();
            services.AddScoped<IRequestHandler<CambiarHabilitacionPuntoAccesoCommand, PuntoAccesoDTO?>, CambiarHabilitacionPuntoAccesoUseCase>();
            services.AddScoped<IRequestHandler<ObtenerPuntoAccesoPorIdQuery, PuntoAccesoDTO?>, ObtenerPuntoAccesoPorIdUseCase>();
            services.AddScoped<IRequestHandler<ListarPuntosAccesoQuery, PaginationResponse<PuntoAccesoDTO>>, ListarPuntosAccesoUseCase>();

            return services;
        }
    }
}
