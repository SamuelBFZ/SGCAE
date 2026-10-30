using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Exceptions;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.PuntosAcceso.Commands.RegistrarPuntoAcceso
{
    public class RegistrarPuntoAccesoUseCase : IRequestHandler<RegistrarPuntoAccesoCommand, PuntoAccesoDTO>
    {
        private readonly IPuntosAccesoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarPuntoAccesoUseCase(IPuntosAccesoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PuntoAccesoDTO> Handle(RegistrarPuntoAccesoCommand command)
        {
            PuntoAcceso punto = PuntoAcceso.Registrar(command.Nombre);

            PuntoAcceso? existente = await _repository.GetByNombreAsync(punto.Nombre);
            if (existente is not null)
            {
                throw new ConflictException("Ya existe un punto de acceso con ese nombre.");
            }

            await _repository.AddAsync(punto);
            await _unitOfWork.CommitAsync();

            return punto.ToDto();
        }
    }
}
