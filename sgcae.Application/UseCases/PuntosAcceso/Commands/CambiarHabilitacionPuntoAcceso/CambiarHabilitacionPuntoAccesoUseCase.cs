using sgcae.Application.Contracts.Persistence;
using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.PuntosAcceso.Commands.CambiarHabilitacionPuntoAcceso
{
    public class CambiarHabilitacionPuntoAccesoUseCase : IRequestHandler<CambiarHabilitacionPuntoAccesoCommand, PuntoAccesoDTO?>
    {
        private readonly IPuntosAccesoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CambiarHabilitacionPuntoAccesoUseCase(IPuntosAccesoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PuntoAccesoDTO?> Handle(CambiarHabilitacionPuntoAccesoCommand command)
        {
            PuntoAcceso? punto = await _repository.GetByIdAsync(command.Id);
            if (punto is null)
            {
                return null;
            }

            if (command.Habilitado)
            {
                punto.Habilitar();
            }
            else
            {
                punto.Deshabilitar();
            }

            _repository.Update(punto);
            await _unitOfWork.CommitAsync();

            return punto.ToDto();
        }
    }
}
