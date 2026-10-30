# Empleados

Base: `$base = "http://localhost:5210"`.

La respuesta de un empleado trae `id`, `identificacion`, `nombres`, `apellidos`, `correo`, `cargo`, `area` y `estadoLaboral` (`Activo` o `Inactivo`).

## Registrar

`POST /api/empleados` → 201. El alta deja el estado en `Activo`.

```powershell
curl.exe -i -X POST "$base/api/empleados" -H "Content-Type: application/json" -d '{"identificacion":"100200300","nombres":"Ana","apellidos":"Ruiz","correo":"ana.ruiz@empresa.com","cargo":"Auxiliar de bodega","area":"Almacen"}'
```

Guarda el `id` de la respuesta:

```powershell
$empleadoId = "11111111-1111-1111-1111-111111111111"
```

## Nombre vacío

400. El mensaje indica el campo que faltó. El mismo código aplica a identificación, apellidos, correo, cargo o área en blanco.

```powershell
curl.exe -i -X POST "$base/api/empleados" -H "Content-Type: application/json" -d '{"identificacion":"100200301","nombres":" ","apellidos":"Ruiz","correo":"ana.ruiz@empresa.com","cargo":"Auxiliar de bodega","area":"Almacen"}'
```

## Correo inválido

400. El mensaje es `El correo no tiene un formato válido.`

```powershell
curl.exe -i -X POST "$base/api/empleados" -H "Content-Type: application/json" -d '{"identificacion":"100200302","nombres":"Ana","apellidos":"Ruiz","correo":"ana-ruiz","cargo":"Auxiliar de bodega","area":"Almacen"}'
```

## Identificación duplicada

409. El mensaje es `Ya existe un empleado con esa identificación.`

```powershell
curl.exe -i -X POST "$base/api/empleados" -H "Content-Type: application/json" -d '{"identificacion":"100200300","nombres":"Ana","apellidos":"Ruiz","correo":"otra.ana@empresa.com","cargo":"Auxiliar de bodega","area":"Almacen"}'
```

## Consultar por id

`GET /api/empleados/{id}` → 200.

```powershell
curl.exe -i "$base/api/empleados/$empleadoId"
```

## Consultar un id que no existe

404.

```powershell
curl.exe -i "$base/api/empleados/00000000-0000-0000-0000-000000000001"
```

## Consultar un id mal formado

400. El valor no tiene formato de guid.

```powershell
curl.exe -i "$base/api/empleados/no-es-un-guid"
```

## Listar

`GET /api/empleados` → 200. Sin filtros trae activos e inactivos, de a 15.

```powershell
curl.exe -i "$base/api/empleados"
```

## Listar solo activos

```powershell
curl.exe -i "$base/api/empleados?estado=Activo"
```

## Listar solo inactivos

```powershell
curl.exe -i "$base/api/empleados?estado=Inactivo"
```

## Listar con página y tamaño

`pageSize` por encima de 50 se recorta a 50. `pageNumber` menor que 1 se trata como 1.

```powershell
curl.exe -i "$base/api/empleados?pageNumber=1&pageSize=5"
curl.exe -i "$base/api/empleados?pageNumber=2&pageSize=5"
curl.exe -i "$base/api/empleados?pageNumber=0&pageSize=80"
```

## Estado de listado inválido

400. Solo se aceptan `Activo` e `Inactivo`.

```powershell
curl.exe -i "$base/api/empleados?estado=Suspendido"
```

## Actualizar

`PUT /api/empleados/{id}` → 200. No cambia la identificación ni el estado laboral.

```powershell
curl.exe -i -X PUT "$base/api/empleados/$empleadoId" -H "Content-Type: application/json" -d '{"nombres":"Ana Maria","apellidos":"Ruiz","correo":"ana.maria.ruiz@empresa.com","cargo":"Coordinadora de bodega","area":"Almacen"}'
```

## Actualizar un empleado que no existe

404.

```powershell
curl.exe -i -X PUT "$base/api/empleados/00000000-0000-0000-0000-000000000001" -H "Content-Type: application/json" -d '{"nombres":"Ana","apellidos":"Ruiz","correo":"ana.ruiz@empresa.com","cargo":"Auxiliar de bodega","area":"Almacen"}'
```

## Desactivar

`POST /api/empleados/{id}/desactivar` → 200. No borra el registro. `estadoLaboral` queda en `Inactivo`. Llamarlo de nuevo sigue respondiendo 200.

```powershell
curl.exe -i -X POST "$base/api/empleados/$empleadoId/desactivar"
```

## Desactivar un empleado que no existe

404.

```powershell
curl.exe -i -X POST "$base/api/empleados/00000000-0000-0000-0000-000000000001/desactivar"
```

## Listar credenciales del empleado

`GET /api/empleados/{id}/credenciales` → 200 con la lista, vacía si todavía no tiene credenciales. 404 si el empleado no existe.

```powershell
curl.exe -i "$base/api/empleados/$empleadoId/credenciales"
curl.exe -i "$base/api/empleados/00000000-0000-0000-0000-000000000001/credenciales"
```
