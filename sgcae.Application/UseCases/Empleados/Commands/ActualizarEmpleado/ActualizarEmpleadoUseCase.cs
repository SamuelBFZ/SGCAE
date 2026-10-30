using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Empleados.Commands.ActualizarEmpleado
{
    public class ActualizarEmpleadoUseCase : IRequestHandler<ActualizarEmpleadoCommand, EmpleadoDTO?>
    {
        private readonly IEmpleadosRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarEmpleadoUseCase(IEmpleadosRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmpleadoDTO?> Handle(ActualizarEmpleadoCommand command)
        {
            Empleado? empleado = await _repository.GetByIdAsync(command.Id);
            if (empleado is null)
            {
                return null;
            }

            empleado.ActualizarDatos(command.Nombres, command.Apellidos, command.Correo, command.Cargo, command.Area);
            _repository.Update(empleado);
            await _unitOfWork.CommitAsync();

            return empleado.ToDto();
        }
    }
}
