using Microsoft.AspNetCore.Mvc;
using sgcae.Application.UseCases.Credenciales;
using sgcae.Application.UseCases.Credenciales.Commands.AsignarCredencial;
using sgcae.Application.UseCases.Credenciales.Commands.BloquearCredencial;
using sgcae.Application.UseCases.Credenciales.Commands.DesbloquearCredencial;
using sgcae.Application.UseCases.Credenciales.Commands.RevocarCredencial;
using sgcae.Application.UseCases.Credenciales.Queries.ObtenerCredencialPorCodigo;
using sgcae.Application.UseCases.Credenciales.Queries.ObtenerCredencialPorId;
using sgcae.Application.Utilities.Mediator;

namespace sgcae.Api.Controllers
{
    [Route("api/credenciales")]
    [ApiController]
    public class CredencialesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CredencialesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Asignar([FromBody] AsignarCredencialRequest request)
        {
            CredencialDTO? creada = await _mediator.Send(new AsignarCredencialCommand
            {
                EmpleadoId = request.EmpleadoId,
                Codigo = request.Codigo
            });

            if (creada is null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            CredencialDTO? credencial = await _mediator.Send(new ObtenerCredencialPorIdQuery { Id = id });
            if (credencial is null)
            {
                return NotFound();
            }

            return Ok(credencial);
        }

        [HttpGet("por-codigo/{codigo}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        {
            CredencialDTO? credencial = await _mediator.Send(new ObtenerCredencialPorCodigoQuery { Codigo = codigo });
            if (credencial is null)
            {
                return NotFound();
            }

            return Ok(credencial);
        }

        [HttpPost("{id:guid}/bloquear")]
        public async Task<IActionResult> Bloquear(Guid id)
        {
            return await CambiarEstado(new BloquearCredencialCommand { Id = id });
        }

        [HttpPost("{id:guid}/desbloquear")]
        public async Task<IActionResult> Desbloquear(Guid id)
        {
            return await CambiarEstado(new DesbloquearCredencialCommand { Id = id });
        }

        [HttpPost("{id:guid}/revocar")]
        public async Task<IActionResult> Revocar(Guid id)
        {
            return await CambiarEstado(new RevocarCredencialCommand { Id = id });
        }

        private async Task<IActionResult> CambiarEstado<TCommand>(TCommand command)
            where TCommand : IRequest<CredencialDTO?>
        {
            CredencialDTO? credencial = await _mediator.Send(command);
            if (credencial is null)
            {
                return NotFound();
            }

            return Ok(credencial);
        }
    }

    public class AsignarCredencialRequest
    {
        public Guid EmpleadoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }
}
