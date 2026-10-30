# SGCAE

Sistema de Gestión y Control de Acceso de Empleados. API REST para registrar empleados, credenciales y puntos de acceso, y para dejar trazabilidad de cada entrada o salida, autorizada o rechazada.

Stack: .NET 10, ASP.NET Core, Entity Framework Core y SQL Server. La solución separa dominio, casos de uso y persistencia. Los comandos y las consultas viven en carpetas distintas.

## Solución

```text
sgcae.slnx
├── sgcae.Api            controladores y OpenAPI
├── sgcae.Application    comandos, consultas, DTOs y contratos
├── sgcae.Domain         entidades y reglas
└── sgcae.Persistence    EF Core, SQL Server y migraciones
```

`sgcae.Api` referencia Application y Persistence. Persistence referencia Application y Domain. Application referencia Domain. Domain no referencia a ningún otro proyecto.

## Ejecutar

Hace falta el SDK de .NET 10 y una instancia de SQL Server LocalDB. La cadena de conexión está en `sgcae.Api/appsettings.json` (`ConnectionStrings:MyConnection`) y apunta a la base `SgcaeDb`.

Desde esta carpeta:

```powershell
dotnet tool restore
dotnet run --project sgcae.Api --launch-profile http
```

Al arrancar se aplican las migraciones. La API queda en `http://localhost:5210`. En Development el documento OpenAPI está en `http://localhost:5210/openapi/v1.json`.

El perfil `http` evita la redirección a HTTPS, así que los ejemplos de curl pueden usar ese puerto directo.

## Recursos

| Recurso | Ruta | Qué permite |
| --- | --- | --- |
| Empleados | `/api/empleados` | Registrar, consultar, actualizar y desactivar |
| Credenciales | `/api/credenciales` | Asignar, consultar, bloquear, desbloquear y revocar |
| Puntos de acceso | `/api/puntos-acceso` | Registrar, consultar y habilitar o deshabilitar |
| Movimientos | `/api/movimientos` | Registrar un acceso y consultar el historial |

Los listados paginados usan `pageNumber` (menor que 1 se toma como 1) y `pageSize` (por defecto 15, máximo 50). La respuesta incluye `items`, `totalCount`, `pageNumber`, `pageSize`, `totalPages`, `hasPreviousPage` y `hasNextPage`.

Los enumerados viajan como texto: `Activo`, `Inactivo`, `Vigente`, `Bloqueada`, `Revocada`, `Entrada`, `Salida`, `Autorizado`, `Rechazado`.

Un error de regla de negocio responde 400 con `{ "message": "..." }`. Un conflicto de unicidad o de transición de credencial responde 409 con el mismo sobre. Un recurso inexistente responde 404. Un guid o un enumerado mal formado responde 400 con el detalle de validación del framework.

## Ejemplos curl

Todas las llamadas de la API, con sus variantes de filtro y de error, están en [docs/curls](docs/curls/README.md):

- [Empleados](docs/curls/empleados.md)
- [Credenciales](docs/curls/credenciales.md)
- [Puntos de acceso](docs/curls/puntos-acceso.md)
- [Movimientos e historial](docs/curls/movimientos.md)
