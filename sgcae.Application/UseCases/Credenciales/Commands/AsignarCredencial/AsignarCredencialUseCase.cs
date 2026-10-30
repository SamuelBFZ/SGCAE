using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Exceptions;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Credenciales.Commands.AsignarCredencial
{
    public class AsignarCredencialUseCase : IRequestHandler<AsignarCredencialCommand, CredencialDTO?>
    {
        private readonly IEmpleadosRepository _empleados;
        private readonly ICredencialesRepository _credenciales;
        private readonly IUnitOfWork _unitOfWork;

        public AsignarCredencialUseCase(
            IEmpleadosRepository empleados,
            ICredencialesRepository credenciales,
            IUnitOfWork unitOfWork)
        {
            _empleados = empleados;
            _credenciales = credenciales;
            _unitOfWork = unitOfWork;
        }

        public async Task<CredencialDTO?> Handle(AsignarCredencialCommand command)
        {
            Credencial credencial = Credencial.Asignar(command.EmpleadoId, command.Codigo);

            Empleado? empleado = await _empleados.GetByIdAsync(command.EmpleadoId);
            if (empleado is null)
            {
                return null;
            }

            Credencial? existente = await _credenciales.GetByCodigoAsync(credencial.Codigo);
            if (existente is not null)
            {
                throw new ConflictException("Ya existe una credencial con ese código.");
            }

            await _credenciales.AddAsync(credencial);
            await _unitOfWork.CommitAsync();

            return credencial.ToDto();
        }
    }
}
