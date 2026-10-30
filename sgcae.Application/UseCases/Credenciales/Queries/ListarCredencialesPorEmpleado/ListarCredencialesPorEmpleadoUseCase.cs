using sgcae.Application.Contracts.Repositories;
using sgcae.Application.Utilities.Mediator;
using sgcae.Domain.Entities;

namespace sgcae.Application.UseCases.Credenciales.Queries.ListarCredencialesPorEmpleado
{
    public class ListarCredencialesPorEmpleadoUseCase : IRequestHandler<ListarCredencialesPorEmpleadoQuery, IReadOnlyList<CredencialDTO>?>
    {
        private readonly IEmpleadosRepository _empleados;
        private readonly ICredencialesRepository _credenciales;

        public ListarCredencialesPorEmpleadoUseCase(IEmpleadosRepository empleados, ICredencialesRepository credenciales)
        {
            _empleados = empleados;
            _credenciales = credenciales;
        }

        public async Task<IReadOnlyList<CredencialDTO>?> Handle(ListarCredencialesPorEmpleadoQuery query)
        {
            Empleado? empleado = await _empleados.GetByIdAsync(query.EmpleadoId);
            if (empleado is null)
            {
                return null;
            }

            IReadOnlyList<Credencial> credenciales = await _credenciales.ListByEmpleadoAsync(empleado.Id);
            return credenciales.Select(credencial => credencial.ToDto()).ToList();
        }
    }
}
