@echo off
set PROJECT_TO_PUBLISH=WooCommerce.NET.csproj
set PATH_TO_PROJECT=src\WooCommerce.NET\
set NUGET_SOURCE_URL=https://nuget.pkg.github.com/skuvault-integrations/index.json

cd %PATH_TO_PROJECT%
dotnet clean %PROJECT_TO_PUBLISH% -c Release
del /S /Q bin\Release\*

dotnet build %PROJECT_TO_PUBLISH% -c Release
echo --- package built

cd bin\Release
IF %1.==. GOTO NoApiKey

set API_KEY=%1
dotnet nuget push *.nupkg --api-key %API_KEY% --source %NUGET_SOURCE_URL%
GOTO End1

:NoApiKey
echo No GitHub personal access token (PAT) passed as a parameter, using interactive mode.
dotnet nuget push *.nupkg --source %NUGET_SOURCE_URL% --interactive

:End1
echo --- publishing completed
PAUSE