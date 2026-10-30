using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Movimientos.Commands.RegistrarMovimientoAcceso
{
    public class RegistrarMovimientoAccesoUseCase : IRequestHandler<RegistrarMovimientoAccesoCommand, MovimientoAccesoDTO>
    {
        private readonly ICredencialesRepository _credenciales;
        private readonly IEmpleadosRepository _empleados;
        private readonly IPuntosAccesoRepository _puntos;
        private readonly IMovimientosAccesoRepository _movimientos;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarMovimientoAccesoUseCase(
            ICredencialesRepository credenciales,
            IEmpleadosRepository empleados,
            IPuntosAccesoRepository puntos,
            IMovimientosAccesoRepository movimientos,
            IUnitOfWork unitOfWork)
        {
            _credenciales = credenciales;
            _empleados = empleados;
            _puntos = puntos;
            _movimientos = movimientos;
            _unitOfWork = unitOfWork;
        }

        public async Task<MovimientoAccesoDTO> Handle(RegistrarMovimientoAccesoCommand command)
        {
            Credencial? credencial = string.IsNullOrWhiteSpace(command.CodigoCredencial)
                ? null
                : await _credenciales.GetByCodigoAsync(command.CodigoCredencial);

            Empleado? empleado = credencial is null
                ? null
                : await _empleados.GetByIdAsync(credencial.EmpleadoId);

            PuntoAcceso? punto = command.PuntoAccesoId == Guid.Empty
                ? null
                : await _puntos.GetByIdAsync(command.PuntoAccesoId);

            MovimientoAcceso movimiento = MovimientoAcceso.Registrar(
                command.CodigoCredencial,
                command.PuntoAccesoId,
                command.Tipo,
                credencial,
                empleado,
                punto,
                DateTime.UtcNow);

            await _movimientos.AddAsync(movimiento);
            await _unitOfWork.CommitAsync();

            return movimiento.ToDto();
        }
    }
}
