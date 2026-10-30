using Microsoft.AspNetCore.Mvc;
using sgcae.Application.UseCases.Movimientos;
using sgcae.Application.UseCases.Movimientos.Commands.RegistrarMovimientoAcceso;
using sgcae.Application.UseCases.Movimientos.Queries.ListarHistorialAccesos;
using sgcae.Application.UseCases.Movimientos.Queries.ObtenerMovimientoPorId;
using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;
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

        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponse<HistorialAccesoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Listar(
            [FromQuery] Guid? empleadoId,
            [FromQuery] DateTime? desde,
            [FromQuery] DateTime? hasta,
            [FromQuery] Guid? puntoAccesoId,
            [FromQuery] ResultadoMovimiento? resultado,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = PaginationRequest.DefaultPageSize)
        {
            PaginationResponse<HistorialAccesoDTO> page = await _mediator.Send(new ListarHistorialAccesosQuery
            {
                EmpleadoId = empleadoId,
                Desde = desde,
                Hasta = hasta,
                PuntoAccesoId = puntoAccesoId,
                Resultado = resultado,
                Pagination = new PaginationRequest(pageNumber, pageSize)
            });

            return Ok(page);
        }
    }

    public class RegistrarMovimientoRequest
    {
        public string CodigoCredencial { get; set; } = string.Empty;
        public Guid PuntoAccesoId { get; set; }
        public TipoMovimiento Tipo { get; set; }
    }
}
