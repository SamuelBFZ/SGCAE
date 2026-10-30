# Movimientos e historial

Base: `$base = "http://localhost:5210"`. Hacen falta `$empleadoId`, `$credencialId` y `$puntoId` de los otros archivos. La credencial debe estar `Vigente`, el empleado `Activo` y el punto habilitado para obtener un acceso autorizado.

Registrar siempre persiste el intento cuando la solicitud está bien formada. Un rechazo de negocio responde **201**, no 400. El 400 queda para un tipo inválido, un guid mal formado, un código vacío o un punto vacío.

## Entrada autorizada

`POST /api/movimientos` → 201. `resultado` es `Autorizado` y `motivo` es null.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d "{\"codigoCredencial\":\"CRD-1001\",\"puntoAccesoId\":\"$puntoId\",\"tipo\":\"Entrada\"}"
```

```powershell
$movimientoId = "44444444-4444-4444-4444-444444444444"
```

## Salida autorizada

Misma validación que la entrada. No se exige una entrada previa.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d "{\"codigoCredencial\":\"CRD-1001\",\"puntoAccesoId\":\"$puntoId\",\"tipo\":\"Salida\"}"
```

## Credencial que no existe

201. `resultado` es `Rechazado`, `motivo` es `CredencialNoExiste` y se conserva el código enviado. `empleadoId` queda null.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d "{\"codigoCredencial\":\"CRD-NO-EXISTE\",\"puntoAccesoId\":\"$puntoId\",\"tipo\":\"Entrada\"}"
```

## Credencial bloqueada o revocada

201. `motivo` es `CredencialNoUtilizable`. Bloquea o revoca la credencial antes, con los curl de [credenciales.md](credenciales.md).

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d "{\"codigoCredencial\":\"CRD-1001\",\"puntoAccesoId\":\"$puntoId\",\"tipo\":\"Entrada\"}"
```

## Empleado inactivo

201. `motivo` es `EmpleadoInactivo`. La credencial sigue vigente y el empleado ya fue desactivado.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d "{\"codigoCredencial\":\"CRD-1001\",\"puntoAccesoId\":\"$puntoId\",\"tipo\":\"Entrada\"}"
```

## Punto que no existe

201. `motivo` es `PuntoNoExiste`. `puntoAccesoId` queda null y `puntoAccesoSolicitado` conserva el guid enviado.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d '{"codigoCredencial":"CRD-1001","puntoAccesoId":"55555555-5555-5555-5555-555555555555","tipo":"Entrada"}'
```

## Punto deshabilitado

201. `motivo` es `PuntoDeshabilitado`. Deshabilita el punto antes, con el PATCH de [puntos-acceso.md](puntos-acceso.md).

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d "{\"codigoCredencial\":\"CRD-1001\",\"puntoAccesoId\":\"$puntoId\",\"tipo\":\"Salida\"}"
```

## Tipo inválido

400. No se inserta fila. El tipo solo puede ser `Entrada` o `Salida`.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d "{\"codigoCredencial\":\"CRD-1001\",\"puntoAccesoId\":\"$puntoId\",\"tipo\":\"Permanencia\"}"
```

## Código vacío

400. El mensaje es `El código de la credencial es requerido.` No se inserta fila.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d "{\"codigoCredencial\":\" \",\"puntoAccesoId\":\"$puntoId\",\"tipo\":\"Entrada\"}"
```

## Punto vacío

400. El mensaje es `El punto de acceso es requerido.` No se inserta fila.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d '{"codigoCredencial":"CRD-1001","puntoAccesoId":"00000000-0000-0000-0000-000000000000","tipo":"Entrada"}'
```

## Punto mal formado

400. El valor no tiene formato de guid y no se inserta fila.

```powershell
curl.exe -i -X POST "$base/api/movimientos" -H "Content-Type: application/json" -d '{"codigoCredencial":"CRD-1001","puntoAccesoId":"no-es-un-guid","tipo":"Entrada"}'
```

## Consultar un movimiento por id

`GET /api/movimientos/{id}` → 200, o 404 si no existe. Este GET no se reemplaza por el listado.

```powershell
curl.exe -i "$base/api/movimientos/$movimientoId"
curl.exe -i "$base/api/movimientos/00000000-0000-0000-0000-000000000001"
```

## Historial sin filtros

`GET /api/movimientos` → 200. Primera página de todo el historial, autorizados y rechazados. `totalCount` es el total de la tabla, no el de la página.

```powershell
curl.exe -i "$base/api/movimientos"
```

## Historial por empleado

Incluye autorizados y rechazados de ese empleado.

```powershell
curl.exe -i "$base/api/movimientos?empleadoId=$empleadoId"
```

## Historial por rango de fechas

`desde` y `hasta` son inclusivos. Puede enviarse solo uno de los dos.

```powershell
curl.exe -i "$base/api/movimientos?desde=2026-10-30T00:00:00Z&hasta=2026-10-30T23:59:59Z"
curl.exe -i "$base/api/movimientos?desde=2026-10-30T00:00:00Z"
curl.exe -i "$base/api/movimientos?hasta=2026-10-30T23:59:59Z"
```

## Historial por punto de acceso

Solo movimientos cuyo punto resuelto es ese id.

```powershell
curl.exe -i "$base/api/movimientos?puntoAccesoId=$puntoId"
```

## Historial por resultado

```powershell
curl.exe -i "$base/api/movimientos?resultado=Autorizado"
curl.exe -i "$base/api/movimientos?resultado=Rechazado"
```

## Historial con los cuatro filtros

El total baja respecto de cada filtro por separado, porque se aplican a la vez.

```powershell
curl.exe -i "$base/api/movimientos?empleadoId=$empleadoId&desde=2026-10-30T00:00:00Z&hasta=2026-10-30T23:59:59Z&puntoAccesoId=$puntoId&resultado=Autorizado"
```

## Segunda página

`pageSize` pequeño para ver que no viaja el historial completo. La página 2 no repite los ítems de la página 1. Un `pageSize` mayor que 50 se recorta a 50.

```powershell
curl.exe -i "$base/api/movimientos?pageNumber=1&pageSize=2"
curl.exe -i "$base/api/movimientos?pageNumber=2&pageSize=2"
curl.exe -i "$base/api/movimientos?pageSize=80"
```

## Rango invertido

400. El mensaje es `La fecha inicial no puede ser posterior a la fecha final.` No devuelve datos parciales.

```powershell
curl.exe -i "$base/api/movimientos?desde=2026-10-31T00:00:00Z&hasta=2026-10-01T00:00:00Z"
```

## Resultado inválido

400. Solo se aceptan `Autorizado` y `Rechazado`.

```powershell
curl.exe -i "$base/api/movimientos?resultado=Pendiente"
```

## Guid de filtro mal formado

400.

```powershell
curl.exe -i "$base/api/movimientos?empleadoId=no-es-un-guid"
curl.exe -i "$base/api/movimientos?puntoAccesoId=no-es-un-guid"
```

Cada ítem del historial trae `id`, `tipo`, `resultado`, `motivo`, `codigoCredencial`, `empleadoId`, `puntoAccesoId`, `puntoAccesoSolicitado`, `nombrePunto` y `ocurridoEn`. Si el punto no existía, `nombrePunto` llega null y `puntoAccesoSolicitado` conserva el id enviado.
