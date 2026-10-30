@echo off

setlocal EnableExtensions

set "ROOT=%~dp0"
set "PERSISTENCE=%ROOT%sgcae.Persistence\sgcae.Persistence.csproj"
set "API=%ROOT%sgcae.Api\sgcae.Api.csproj"
set "MIGRATIONS_DIR=Migrations"

cd /d "%ROOT%"

if "%~1" == "" goto usage
if "%~1" == "help" goto usage
if "%~1" == "h" goto usage
if "%~1" == "--help" goto usage

if "%~1" == "add" goto add
if "%~1" == "update" goto update

echo Comando desconocido: %~1
goto usage

:add
if "%~2" == "" (
	echo Error: falta el nombre de la migracion.
	goto usage
)
dotnet tool run dotnet-ef migrations add "%~2" --project "%PERSISTENCE%" --startup-project "%API%" --output-dir "%MIGRATIONS_DIR%"
goto end

:update
if "%~2" == "" (
	dotnet tool run dotnet-ef database update --project "%PERSISTENCE%" --startup-project "%API%"
) else (
	dotnet tool run dotnet-ef database update "%~2" --project "%PERSISTENCE%" --startup-project "%API%"
)
goto end

:usage
echo Uso: ef.cmd ^<comando^> [args]
echo.
echo Comandos:
echo    add ^<Nombre^>			Crea una migracion
echo    update [Nombre]		Aplica migraciones (todas o por nombre)
exit /b 1

:end
endlocal
