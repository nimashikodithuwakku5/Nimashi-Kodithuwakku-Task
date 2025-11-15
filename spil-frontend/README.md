# Frontend — SPIL Sales Order (React + Vite)

This file documents the frontend portion of the SPIL Sales Order app and includes exact commands to run the frontend, backend and database so other developers can reproduce the same output locally.

## Tech stack & versions
- Frontend: React (with Vite) — tested with Node 18+ and Vite (v3+ / v4+ compatible). Use `npm` for package management.
-- Backend: .NET 8.0 (SDK 8.0.x) and Entity Framework Core (code-first migrations). This project targets `net8.0`.

	Install or update the .NET 8 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0
- Database: Microsoft SQL Server (database name used in this solution: `SPILSalesOrderDb`). LocalDB or Docker-based SQL Server are supported.

If you need to confirm the installed .NET SDK on your machine, run:

```powershell
dotnet --version
# Example expected output for this project: 8.0.x (for example: 8.0.100)
```

## Files and locations (important)
- Frontend root: `spil-frontend`
- Backend API: `backend/src/SPILSalesOrder.API`
- Backend EF project: `backend/src/SPILSalesOrder.Infrastructure`
- Seeder tool (development data): `backend/tools/SeedClients`

## Database name
This project expects a database named `SPILSalesOrderDb` by default. You can change the name in the connection string inside `backend/src/SPILSalesOrder.API/appsettings.Development.json`.

## Run the database (recommended approaches)

Option A — Docker (recommended for reproducible local environments):

```powershell
# Run SQL Server 2019 container (example)
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 --name sqlserver-local -d mcr.microsoft.com/mssql/server:2019-latest

# Wait for the database to be ready, then use the connection string:
# Server=localhost,1433;User Id=sa;Password=Your_password123;Database=SPILSalesOrderDb;MultipleActiveResultSets=true
```

Option B — LocalDB (Windows dev machines):

```json
"ConnectionStrings": {
	"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SPILSalesOrderDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

> Note: Do not commit secret passwords. Use `appsettings.Development.json.example` or environment variables for sharing.

## Run backend (apply migrations, seed, start)

1. Apply EF Core migrations (from repo root):

```powershell
dotnet ef database update --project "backend/src/SPILSalesOrder.Infrastructure" --startup-project "backend/src/SPILSalesOrder.API"
```

2. Run the seeder (inserts sample Clients and Items used by the UI):

```powershell
cd backend/tools/SeedClients
dotnet run
```

3. Start the backend API (recommended dev URL: `http://localhost:5206`):

```powershell
cd backend/src/SPILSalesOrder.API
dotnet run --urls "http://localhost:5206"
```

If you see a Kestrel error `address already in use`, pick another port or kill the process occupying the port:

```powershell
Get-Process -Name dotnet -ErrorAction SilentlyContinue | Format-Table Id,ProcessName,StartTime -AutoSize
taskkill /F /PID <pid>
```

## Run frontend (Vite)

1. Set the API base URL for the frontend. Create or edit `spil-frontend/.env` with:

```
VITE_API_URL=http://localhost:5206/api
```

2. Install dependencies (first time) and start the dev server:

```powershell
cd spil-frontend
npm install
npm run dev
```

Vite will print the local URL (eg. `http://localhost:5173/`). If that port is in use Vite will try another port (e.g. `5177`). Open the URL printed in the terminal and do a hard refresh (Ctrl+F5) when testing style changes.

## Quick verification commands

- Check clients endpoint (adjust port if changed):

```powershell
Invoke-RestMethod -Uri 'http://localhost:5206/api/clients' -Method Get | ConvertTo-Json -Depth 5
```

- Check items endpoint:

```powershell
Invoke-RestMethod -Uri 'http://localhost:5206/api/items' -Method Get | ConvertTo-Json -Depth 5
```

## Troubleshooting tips
- If frontend styles or component updates are not visible: ensure Vite is running and do a hard refresh (Ctrl+F5). If `.env` changed, restart Vite.
- If backend fails to start: check port bind errors and change the `--urls` port or kill the existing process.
- If EF migrations fail: confirm SQL Server is reachable and connection string credentials are correct.

## Suggested repo additions for reproducibility
- `spil-frontend/.env.example` (example `VITE_API_URL` value)
- `backend/src/SPILSalesOrder.API/appsettings.Development.json.example` (placeholder connection string)
- `docker-compose.yml` to start an SQL Server container and optionally run migrations/seeder
- `scripts/start-dev.ps1` to automate: apply migrations, run seeder, start backend and start frontend (Windows convenience)

---

If you want, I can add any of the suggested reproducibility files (`.env.example`, `appsettings.Development.json.example`, `docker-compose.yml`, or `scripts/start-dev.ps1`) next — tell me which and I'll create them.
