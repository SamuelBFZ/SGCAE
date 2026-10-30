using Microsoft.AspNetCore.Mvc;
using sgcae.Application.UseCases.Credenciales;
using sgcae.Application.UseCases.Credenciales.Queries.ListarCredencialesPorEmpleado;
using sgcae.Application.UseCases.Empleados;
using sgcae.Application.UseCases.Empleados.Commands.ActualizarEmpleado;
using sgcae.Application.UseCases.Empleados.Commands.DesactivarEmpleado;
using sgcae.Application.UseCases.Empleados.Commands.RegistrarEmpleado;
using sgcae.Application.UseCases.Empleados.Queries.ListarEmpleados;
using sgcae.Application.UseCases.Empleados.Queries.ObtenerEmpleadoPorId;
using sgcae.Application.Utilities.Mediator;
using sgcae.Application.Utilities.Pagination;
using sgcae.Domain.Entities;

namespace sgcae.Api.Controllers
{
    [Route("api/empleados")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpleadosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] RegistrarEmpleadoRequest request)
        {
            EmpleadoDTO creado = await _mediator.Send(new RegistrarEmpleadoCommand
            {
                Identificacion = request.Identificacion,
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Correo = request.Correo,
                Cargo = request.Cargo,
                Area = request.Area
            });

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            EmpleadoDTO? empleado = await _mediator.Send(new ObtenerEmpleadoPorIdQuery { Id = id });
            if (empleado is null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = PaginationRequest.DefaultPageSize,
            [FromQuery] EstadoLaboral? estado = null)
        {
            PaginationResponse<EmpleadoDTO> page = await _mediator.Send(new ListarEmpleadosQuery
            {
                Pagination = new PaginationRequest(pageNumber, pageSize),
                Estado = estado
            });

            return Ok(page);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarEmpleadoRequest request)
        {
            EmpleadoDTO? empleado = await _mediator.Send(new ActualizarEmpleadoCommand
            {
                Id = id,
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Correo = request.Correo,
                Cargo = request.Cargo,
                Area = request.Area
            });

            if (empleado is null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        [HttpGet("{empleadoId:guid}/credenciales")]
        public async Task<IActionResult> ListarCredenciales(Guid empleadoId)
        {
            IReadOnlyList<CredencialDTO>? credenciales = await _mediator.Send(new ListarCredencialesPorEmpleadoQuery
            {
                EmpleadoId = empleadoId
            });

            if (credenciales is null)
            {
                return NotFound();
            }

            return Ok(credenciales);
        }

        [HttpPost("{id:guid}/desactivar")]
        public async Task<IActionResult> Desactivar(Guid id)
        {
            EmpleadoDTO? empleado = await _mediator.Send(new DesactivarEmpleadoCommand { Id = id });
            if (empleado is null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }
    }

    public class RegistrarEmpleadoRequest
    {
        public string Identificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
    }

    public class ActualizarEmpleadoRequest
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
    }
}
