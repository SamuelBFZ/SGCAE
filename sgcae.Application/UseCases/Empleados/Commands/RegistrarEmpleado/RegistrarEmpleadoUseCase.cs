using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Exceptions;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Empleados.Commands.RegistrarEmpleado
{
    public class RegistrarEmpleadoUseCase : IRequestHandler<RegistrarEmpleadoCommand, EmpleadoDTO>
    {
        private readonly IEmpleadosRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarEmpleadoUseCase(IEmpleadosRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmpleadoDTO> Handle(RegistrarEmpleadoCommand command)
        {
            Empleado empleado = Empleado.Registrar(
                command.Identificacion,
                command.Nombres,
                command.Apellidos,
                command.Correo,
                command.Cargo,
                command.Area);

            Empleado? existente = await _repository.GetByIdentificacionAsync(empleado.Identificacion);
            if (existente is not null)
            {
                throw new ConflictException("Ya existe un empleado con esa identificación.");
            }

            await _repository.AddAsync(empleado);
            await _unitOfWork.CommitAsync();

            return empleado.ToDto();
        }
    }
}
