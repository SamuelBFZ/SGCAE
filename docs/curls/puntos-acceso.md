# Puntos de acceso

Base: `$base = "http://localhost:5210"`.

La respuesta trae `id`, `nombre` y `habilitado`. El alta deja `habilitado` en verdadero. Recepción, parqueadero, bodega y área restringida son ejemplos de nombre: no hay un catálogo cerrado.

## Registrar

`POST /api/puntos-acceso` → 201.

```powershell
curl.exe -i -X POST "$base/api/puntos-acceso" -H "Content-Type: application/json" -d '{"nombre":"Recepcion"}'
```

```powershell
$puntoId = "33333333-3333-3333-3333-333333333333"
```

Otros puntos útiles para el historial:

```powershell
curl.exe -i -X POST "$base/api/puntos-acceso" -H "Content-Type: application/json" -d '{"nombre":"Parqueadero"}'
curl.exe -i -X POST "$base/api/puntos-acceso" -H "Content-Type: application/json" -d '{"nombre":"Bodega"}'
curl.exe -i -X POST "$base/api/puntos-acceso" -H "Content-Type: application/json" -d '{"nombre":"Area restringida"}'
```

## Nombre vacío

400. El mensaje es `El nombre del punto de acceso es requerido.`

```powershell
curl.exe -i -X POST "$base/api/puntos-acceso" -H "Content-Type: application/json" -d '{"nombre":" "}'
```

## Nombre duplicado

409. El mensaje es `Ya existe un punto de acceso con ese nombre.` Mayúsculas y minúsculas cuentan como el mismo punto: `Recepcion` y `recepcion` chocan.

```powershell
curl.exe -i -X POST "$base/api/puntos-acceso" -H "Content-Type: application/json" -d '{"nombre":"recepcion"}'
```

## Consultar por id

`GET /api/puntos-acceso/{id}` → 200, o 404 si no existe.

```powershell
curl.exe -i "$base/api/puntos-acceso/$puntoId"
curl.exe -i "$base/api/puntos-acceso/00000000-0000-0000-0000-000000000001"
```

## Listar

`GET /api/puntos-acceso` → 200. Sin `habilitado` devuelve habilitados y deshabilitados.

```powershell
curl.exe -i "$base/api/puntos-acceso"
```

## Listar solo habilitados

```powershell
curl.exe -i "$base/api/puntos-acceso?habilitado=true"
```

## Listar solo deshabilitados

```powershell
curl.exe -i "$base/api/puntos-acceso?habilitado=false"
```

## Listar con página y tamaño

```powershell
curl.exe -i "$base/api/puntos-acceso?pageNumber=1&pageSize=2"
curl.exe -i "$base/api/puntos-acceso?pageNumber=2&pageSize=2"
```

## Deshabilitar

`PATCH /api/puntos-acceso/{id}/habilitacion` → 200. El punto sigue existiendo. Repetir el mismo valor responde 200.

```powershell
curl.exe -i -X PATCH "$base/api/puntos-acceso/$puntoId/habilitacion" -H "Content-Type: application/json" -d '{"habilitado":false}'
```

## Volver a habilitar

```powershell
curl.exe -i -X PATCH "$base/api/puntos-acceso/$puntoId/habilitacion" -H "Content-Type: application/json" -d '{"habilitado":true}'
```

## Cambiar la habilitación de un punto que no existe

404.

```powershell
curl.exe -i -X PATCH "$base/api/puntos-acceso/00000000-0000-0000-0000-000000000001/habilitacion" -H "Content-Type: application/json" -d '{"habilitado":false}'
```
