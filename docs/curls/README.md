# Ejemplos curl

Llamadas listas para copiar contra la API en local. Cada archivo cubre un recurso y las respuestas que ese recurso puede dar.

| Archivo | Recurso |
| --- | --- |
| [empleados.md](empleados.md) | Alta, consulta, actualización, desactivación y credenciales del empleado |
| [credenciales.md](credenciales.md) | Asignación, consulta por id o código, bloqueo, desbloqueo y revocación |
| [puntos-acceso.md](puntos-acceso.md) | Alta, consulta, listado filtrado y habilitación |
| [movimientos.md](movimientos.md) | Registro autorizado o rechazado, consulta por id e historial paginado |

## Antes de ejecutar

Arranca la API con el perfil HTTP:

```powershell
dotnet run --project sgcae.Api --launch-profile http
```

En PowerShell, `curl` es un alias de `Invoke-WebRequest`. Los ejemplos usan `curl.exe`.

```powershell
$base = "http://localhost:5210"
```

Los cuerpos van en JSON con nombres en camelCase. Sustituye los guid de ejemplo por los `id` que devuelva cada alta. Un mismo nombre de variable se reutiliza entre archivos: `$empleadoId`, `$credencialId`, `$puntoId` y `$movimientoId`.

## Orden sugerido

1. Crear un punto de acceso y un empleado.
2. Asignar una credencial vigente a ese empleado.
3. Registrar una entrada autorizada.
4. Recorrer los rechazos y los filtros del historial.
5. Probar desactivar, bloquear, revocar y deshabilitar, y volver a registrar un acceso para ver el motivo nuevo.
