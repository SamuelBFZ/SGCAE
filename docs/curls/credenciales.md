# Credenciales

Base: `$base = "http://localhost:5210"`. Hace falta un `$empleadoId` creado en [empleados.md](empleados.md).

La respuesta trae `id`, `codigo`, `empleadoId`, `estado` (`Vigente`, `Bloqueada` o `Revocada`) y `puedeUtilizarse`. Solo `Vigente` puede utilizarse.

## Asignar

`POST /api/credenciales` → 201. El estado inicial es `Vigente`.

```powershell
curl.exe -i -X POST "$base/api/credenciales" -H "Content-Type: application/json" -d "{\"empleadoId\":\"$empleadoId\",\"codigo\":\"CRD-1001\"}"
```

```powershell
$credencialId = "22222222-2222-2222-2222-222222222222"
```

## Código vacío

400. El mensaje es `El código de la credencial es requerido.`

```powershell
curl.exe -i -X POST "$base/api/credenciales" -H "Content-Type: application/json" -d "{\"empleadoId\":\"$empleadoId\",\"codigo\":\" \"}"
```

## Empleado inexistente

404. No se crea la credencial.

```powershell
curl.exe -i -X POST "$base/api/credenciales" -H "Content-Type: application/json" -d '{"empleadoId":"00000000-0000-0000-0000-000000000001","codigo":"CRD-404"}'
```

## Código duplicado

409. El mensaje es `Ya existe una credencial con ese código.`

```powershell
curl.exe -i -X POST "$base/api/credenciales" -H "Content-Type: application/json" -d "{\"empleadoId\":\"$empleadoId\",\"codigo\":\"CRD-1001\"}"
```

## Consultar por id

`GET /api/credenciales/{id}` → 200, o 404 si no existe.

```powershell
curl.exe -i "$base/api/credenciales/$credencialId"
curl.exe -i "$base/api/credenciales/00000000-0000-0000-0000-000000000001"
```

## Consultar por código

`GET /api/credenciales/por-codigo/{codigo}` → 200, o 404 si no existe.

```powershell
curl.exe -i "$base/api/credenciales/por-codigo/CRD-1001"
curl.exe -i "$base/api/credenciales/por-codigo/CRD-NO-EXISTE"
```

## Bloquear

`POST /api/credenciales/{id}/bloquear` → 200. `estado` queda en `Bloqueada` y `puedeUtilizarse` en falso. Bloquear una credencial que ya está bloqueada también responde 200.

```powershell
curl.exe -i -X POST "$base/api/credenciales/$credencialId/bloquear"
```

## Desbloquear

`POST /api/credenciales/{id}/desbloquear` → 200. Solo una credencial bloqueada vuelve a `Vigente`.

```powershell
curl.exe -i -X POST "$base/api/credenciales/$credencialId/desbloquear"
```

## Desbloquear una credencial vigente

409. El mensaje es `Solo se puede desbloquear una credencial bloqueada.`

```powershell
curl.exe -i -X POST "$base/api/credenciales/$credencialId/desbloquear"
```

## Revocar

`POST /api/credenciales/{id}/revocar` → 200. `estado` queda en `Revocada`. Se puede revocar tanto una vigente como una bloqueada.

```powershell
curl.exe -i -X POST "$base/api/credenciales/$credencialId/revocar"
```

## Revocar de nuevo

409. El mensaje es `La credencial ya está revocada.`

```powershell
curl.exe -i -X POST "$base/api/credenciales/$credencialId/revocar"
```

## Bloquear una credencial revocada

409. El mensaje es `Una credencial revocada no puede bloquearse.`

```powershell
curl.exe -i -X POST "$base/api/credenciales/$credencialId/bloquear"
```

## Desbloquear una credencial revocada

409. El mensaje es `Solo se puede desbloquear una credencial bloqueada.`

```powershell
curl.exe -i -X POST "$base/api/credenciales/$credencialId/desbloquear"
```

## Transición sobre un id que no existe

404. Aplica a bloquear, desbloquear y revocar.

```powershell
curl.exe -i -X POST "$base/api/credenciales/00000000-0000-0000-0000-000000000001/bloquear"
curl.exe -i -X POST "$base/api/credenciales/00000000-0000-0000-0000-000000000001/desbloquear"
curl.exe -i -X POST "$base/api/credenciales/00000000-0000-0000-0000-000000000001/revocar"
```
