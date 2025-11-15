<#
  scripts/start-dev.ps1
  Windows helper script to apply EF migrations, run seeder, and start backend and frontend in new PowerShell windows.

  Usage (from repo root):
    .\scripts\start-dev.ps1
    or
    .\scripts\start-dev.ps1 -BackendPort 5206
#>

param(
  [int]$BackendPort = 5206
)

Set-StrictMode -Version Latest

$repoRoot = (Resolve-Path "$(Split-Path -Parent $MyInvocation.MyCommand.Definition)/..").ProviderPath
Write-Host "Repository root: $repoRoot"

Write-Host "Applying EF Core migrations..."
dotnet ef database update --project "$repoRoot\backend\src\SPILSalesOrder.Infrastructure" --startup-project "$repoRoot\backend\src\SPILSalesOrder.API"

if ($LASTEXITCODE -ne 0) {
  Write-Host "Migrations failed. Aborting." -ForegroundColor Red
  exit 1
}

Write-Host "Running DB seeder (in-place)..."
Push-Location "$repoRoot\backend\tools\SeedClients"
dotnet run
Pop-Location

Write-Host "Starting backend in a new PowerShell window (port $BackendPort)..."
Start-Process powershell -ArgumentList "-NoExit","-Command","cd '$repoRoot\backend\src\SPILSalesOrder.API'; dotnet run --urls 'http://localhost:$BackendPort'"

Write-Host "Starting frontend in a new PowerShell window..."
Start-Process powershell -ArgumentList "-NoExit","-Command","cd '$repoRoot\spil-frontend'; npm install; npm run dev"

Write-Host "Done — backend will be available at http://localhost:$BackendPort (see backend window) and frontend Vite output will show the local URL in the frontend window."
