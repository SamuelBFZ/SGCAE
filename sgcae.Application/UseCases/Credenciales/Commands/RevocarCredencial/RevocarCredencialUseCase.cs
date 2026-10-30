using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Credenciales.Commands.RevocarCredencial
{
    public class RevocarCredencialUseCase : IRequestHandler<RevocarCredencialCommand, CredencialDTO?>
    {
        private readonly ICredencialesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RevocarCredencialUseCase(ICredencialesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CredencialDTO?> Handle(RevocarCredencialCommand command)
        {
            Credencial? credencial = await _repository.GetByIdAsync(command.Id);
            if (credencial is null)
            {
                return null;
            }

            CredencialTransicion.Ejecutar(credencial.Revocar);
            _repository.Update(credencial);
            await _unitOfWork.CommitAsync();

            return credencial.ToDto();
        }
    }
}
