using Microsoft.AspNetCore.Mvc;
using sgcae.Application.UseCases.PuntosAcceso;
using sgcae.Application.UseCases.PuntosAcceso.Commands.CambiarHabilitacionPuntoAcceso;
using sgcae.Application.UseCases.PuntosAcceso.Commands.RegistrarPuntoAcceso;
using sgcae.Application.UseCases.PuntosAcceso.Queries.ListarPuntosAcceso;
using sgcae.Application.UseCases.PuntosAcceso.Queries.ObtenerPuntoAccesoPorId;
using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;

namespace sgcae.Api.Controllers
{
    [Route("api/puntos-acceso")]
    [ApiController]
    public class PuntosAccesoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PuntosAccesoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PuntoAccesoDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Registrar([FromBody] RegistrarPuntoAccesoRequest request)
        {
            PuntoAccesoDTO creado = await _mediator.Send(new RegistrarPuntoAccesoCommand
            {
                Nombre = request.Nombre
            });

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PuntoAccesoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            PuntoAccesoDTO? punto = await _mediator.Send(new ObtenerPuntoAccesoPorIdQuery { Id = id });
            if (punto is null)
            {
                return NotFound();
            }

            return Ok(punto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponse<PuntoAccesoDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = PaginationRequest.DefaultPageSize,
            [FromQuery] bool? habilitado = null)
        {
            PaginationResponse<PuntoAccesoDTO> page = await _mediator.Send(new ListarPuntosAccesoQuery
            {
                Pagination = new PaginationRequest(pageNumber, pageSize),
                Habilitado = habilitado
            });

            return Ok(page);
        }

        [HttpPatch("{id:guid}/habilitacion")]
        [ProducesResponseType(typeof(PuntoAccesoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CambiarHabilitacion(Guid id, [FromBody] CambiarHabilitacionRequest request)
        {
            PuntoAccesoDTO? punto = await _mediator.Send(new CambiarHabilitacionPuntoAccesoCommand
            {
                Id = id,
                Habilitado = request.Habilitado
            });

            if (punto is null)
            {
                return NotFound();
            }

            return Ok(punto);
        }
    }

    public class RegistrarPuntoAccesoRequest
    {
        public string Nombre { get; set; } = string.Empty;
    }

    public class CambiarHabilitacionRequest
    {
        public bool Habilitado { get; set; }
    }
}
