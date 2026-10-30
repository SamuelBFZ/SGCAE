using Microsoft.AspNetCore.Mvc;
using sgcae.Application.UseCases.Movimientos;
using sgcae.Application.UseCases.Movimientos.Commands.RegistrarMovimientoAcceso;
using sgcae.Application.UseCases.Movimientos.Queries.ObtenerMovimientoPorId;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Api.Controllers
{
    [Route("api/movimientos")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimientosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] RegistrarMovimientoRequest request)
        {
            MovimientoAccesoDTO creado = await _mediator.Send(new RegistrarMovimientoAccesoCommand
            {
                CodigoCredencial = request.CodigoCredencial,
                PuntoAccesoId = request.PuntoAccesoId,
                Tipo = request.Tipo
            });

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            MovimientoAccesoDTO? movimiento = await _mediator.Send(new ObtenerMovimientoPorIdQuery { Id = id });
            if (movimiento is null)
            {
                return NotFound();
            }

            return Ok(movimiento);
        }
    }

    public class RegistrarMovimientoRequest
    {
        public string CodigoCredencial { get; set; } = string.Empty;
        public Guid PuntoAccesoId { get; set; }
        public TipoMovimiento Tipo { get; set; }
    }
}
