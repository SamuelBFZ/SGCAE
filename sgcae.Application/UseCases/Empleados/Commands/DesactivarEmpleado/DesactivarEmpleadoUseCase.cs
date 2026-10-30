using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Empleados.Commands.DesactivarEmpleado
{
    public class DesactivarEmpleadoUseCase : IRequestHandler<DesactivarEmpleadoCommand, EmpleadoDTO?>
    {
        private readonly IEmpleadosRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DesactivarEmpleadoUseCase(IEmpleadosRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmpleadoDTO?> Handle(DesactivarEmpleadoCommand command)
        {
            Empleado? empleado = await _repository.GetByIdAsync(command.Id);
            if (empleado is null)
            {
                return null;
            }

            empleado.Desactivar();
            _repository.Update(empleado);
            await _unitOfWork.CommitAsync();

            return empleado.ToDto();
        }
    }
}
