**SPIL Sales Order — Setup Guide**

This README explains how to set up and run the full-stack Sales Order application locally so someone cloning from GitHub can reproduce the same output and behavior you see in development.

**Contents**
- Project overview
- Prerequisites
- Quick start (Windows / PowerShell)
- Backend: configure, run migrations, seed data, start API
- Frontend: configure and run Vite dev server
- Using Docker for SQL Server (optional)
- Common troubleshooting
- GitHub / sharing notes

---

Project overview
- Backend: .NET 8 Web API (Clean Architecture) with EF Core. API project is at `backend/src/SPILSalesOrder.API` and Infrastructure project at `backend/src/SPILSalesOrder.Infrastructure`.
- Frontend: Vite + React + Redux + Tailwind located at `spil-frontend`.
- DB: SQL Server (LocalDB or Docker recommended for portability).

---

Prerequisites
- Windows (instructions use PowerShell). macOS/Linux commands are similar but adjust where required.
- .NET 8 SDK installed (dotnet) — verify with `dotnet --version` (should be 8.x).
- EF Core tools (optional): `dotnet tool install --global dotnet-ef` (or use `dotnet ef` as part of SDK if available).
- Node.js and npm (Node 18+ recommended). Verify `node -v` and `npm -v`.
- SQL Server: either LocalDB (built into some developer environments) or a Docker container running SQL Server. See Docker section below.

---

Quick start (clone + one-liners)
1. Clone the repository:

   git clone https://github.com/<your-repo>/spil-salesorder.git
   cd spil-salesorder

2. Configure the backend database connection (see Backend section) and the frontend `.env` (see Frontend section).

3. Apply EF migrations and seed the DB, then run the backend and frontend (detailed commands are in the sections below).

---

Backend setup (detailed)

1) Configure DB connection
- Open `backend/src/SPILSalesOrder.API/appsettings.Development.json` (or `appsettings.json`) and set `ConnectionStrings:DefaultConnection` to a valid SQL Server connection string.

Examples:
- LocalDB (developer machine):

  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SPILSalesOrderDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }

- SQL Server (Docker / standalone) - using default SA credentials (only for local dev):

  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;User Id=sa;Password=Your_password123;Database=SPILSalesOrderDb;MultipleActiveResultSets=true"
  }

Notes:
- Do NOT commit secrets or production credentials into source control. Instead provide a `.env.example` or documentation for developers to set the correct configuration.

Database names (for other developers)
- For clarity: this project expects a SQL Server database named `SPILSalesOrderDB` (note capitalization) when running locally or in CI.
- If you are contributing or running the project locally, create a database called `SPILSalesOrderDB` on your SQL Server instance (for example `localhost\SQLEXPRESS`) and then point `DefaultConnection` at that database.
- IMPORTANT: Do NOT run any scripts or create databases inside other developers' or shared servers without their permission. This note is only to document the expected database name for contributors — it will not create or modify your databases.

2) Run EF Core migrations
- From repo root run (PowerShell):

  dotnet ef database update --project "backend/src/SPILSalesOrder.Infrastructure" --startup-project "backend/src/SPILSalesOrder.API"

This applies the migrations and creates the database schema. If you get errors about `dotnet-ef`, install the tool:

  dotnet tool install --global dotnet-ef

3) Seed sample data
- The project includes a seeder tool to insert sample Clients and Items for development.
- Run:

  cd backend/tools/SeedClients
  dotnet run

The tool will print inserted rows and return. After this, you can verify endpoints like `/api/clients` and `/api/items` return seeded data.

4) Start the backend API
- Recommended dev URL: `http://localhost:5206` (the project may try 5204/5205 — change if occupied).
- Run (from `backend/src/SPILSalesOrder.API` or from repo root):

  cd backend/src/SPILSalesOrder.API
  dotnet run --urls "http://localhost:5206"

If you see a Kestrel binding error (address in use), choose a free port or kill the process occupying the port. Example to find and kill a process on Windows PowerShell:

  # find dotnet processes
  Get-Process -Name dotnet -ErrorAction SilentlyContinue | Format-Table Id,ProcessName,StartTime -AutoSize

  # kill a process by PID (replace <pid>)
  taskkill /F /PID <pid>

---

Frontend setup

1) Configure API base URL
- In `spil-frontend` there is an `.env` (or `.env.example`) file with `VITE_API_URL`.
- Set `VITE_API_URL` to the backend base URL + `/api`, e.g.:

  VITE_API_URL=http://localhost:5206/api

2) Install dependencies and run dev server

  cd spil-frontend
  npm install
  npm run dev

Vite will print the local URL (default 5173; if occupied it will try other ports like 5177). Open the URL printed in the terminal (e.g. http://localhost:5177/) and verify the UI.

Notes:
- If you change `VITE_API_URL` you must restart Vite for it to pick up changes.
- Do a full refresh (Ctrl+F5) to avoid stale caches.

---

Optional: Run SQL Server in Docker (recommended for consistency)

1) Pull and run SQL Server container (example using the Microsoft SQL Server 2019 container):

  docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 --name sqlserver-local -d mcr.microsoft.com/mssql/server:2019-latest

2) Wait for SQL Server to be ready, then use the Docker host connection string (as above) in `appsettings.Development.json`.

3) Run migrations and seeder as described.

---

Troubleshooting (common issues)

- Buttons or CSS not updating:
  - Ensure Vite is running and shows the local URL in terminal.
  - Hard refresh the browser (Ctrl+F5) and clear cache if necessary.
  - If CSS still overrides expected styles, inspect the element in DevTools and check the computed styles to see which rule is taking precedence.

- Backend: Kestrel "address already in use":
  - Use a different port with `--urls "http://localhost:5206"` or kill the process using the port (`taskkill /F /PID <pid>`).

- EF migration errors:
  - Make sure `DefaultConnection` points to an existing SQL Server instance and credentials are valid.
  - If using Docker, ensure the container is running and port 1433 is exposed.

- Seeder fails or does not insert rows:
  - Check the connection string used by the seeder project and run it from the seeder folder. Confirm the `ApplicationDbContext` migrations are applied before seeding.

- CORS / API 404 or connection issues from frontend:
  - Ensure the backend is running and the `VITE_API_URL` is correct (include `/api`). Example: `http://localhost:5206/api`.
  - Confirm the API endpoint paths (e.g., `GET /api/clients`) work by hitting them in the browser or using PowerShell `Invoke-RestMethod -Uri 'http://localhost:5206/api/clients'`.

---

GitHub / sharing notes

- Commit guidance:
  - Do NOT commit production credentials or secrets. Add `appsettings.Development.json` to `.gitignore` if you store secrets locally, or provide an `appsettings.Development.json.example` with placeholder values.
  - Add `spil-frontend/.env` to `.gitignore` and include a `spil-frontend/.env.example` with instructions:

    VITE_API_URL=http://localhost:5206/api

- Onboarding a new developer (recommended checklist):
  1. Clone repo.
  2. Create local SQL Server (LocalDB or Docker) and update `appsettings.Development.json` or use user-secrets.
  3. Run `dotnet ef database update --project backend/src/SPILSalesOrder.Infrastructure --startup-project backend/src/SPILSalesOrder.API`.
  4. Run seeder `dotnet run` in `backend/tools/SeedClients`.
  5. Set `VITE_API_URL` inside `spil-frontend/.env`.
  6. `npm install` and `npm run dev` in `spil-frontend`.

- Optionally provide a `docker-compose.yml` for: SQL Server + migrations/seeding automation. That improves reproducibility for others.

---

Production notes
- For production you should:
  - Use a secure production SQL Server and store credentials in environment variables or a secrets manager.
  - Build the frontend (`npm run build`) and serve the static files either from the backend (configure static file middleware) or using a dedicated static host.
  - Configure HTTPS and reverse proxy (Nginx, IIS, or Azure App Service) and update connection strings and CORS policies accordingly.

---

If anything in this guide is unclear or you want I can:
- Add a `spil-frontend/.env.example` and `backend/appsettings.Development.json.example` to the repo.
- Add a `docker-compose.yml` that starts SQL Server and optionally runs migrations and seeding.
- Add a short script `scripts/start-dev.ps1` that runs the steps (apply migrations, seed, start backend and frontend) for Windows developers.

If you want any of those automated additions, say which one and I will add it to the repo.
