using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Empleados
{
    internal static class EmpleadoMapper
    {
        public static EmpleadoDTO ToDto(this Empleado empleado)
        {
            return new EmpleadoDTO
            {
                Id = empleado.Id,
                Identificacion = empleado.Identificacion,
                Nombres = empleado.Nombres,
                Apellidos = empleado.Apellidos,
                Correo = empleado.Correo,
                Cargo = empleado.Cargo,
                Area = empleado.Area,
                EstadoLaboral = empleado.EstadoLaboral
            };
        }
    }
}
