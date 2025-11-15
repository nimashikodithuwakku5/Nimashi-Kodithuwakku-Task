# Backend (SPILSalesOrder)

This folder contains a Clean-Architecture style .NET 8 Web API backend scaffold for the SPIL Sales Order project.

Structure:
- `src/SPILSalesOrder.Domain` - domain entities
- `src/SPILSalesOrder.Application` - service interfaces and service implementations
- `src/SPILSalesOrder.Infrastructure` - EF Core DbContext and repository implementations
- `src/SPILSalesOrder.API` - Web API (controllers, Program.cs)

To run locally:

1. Ensure .NET 8 SDK is installed and SQL Server is available.
2. Update `appsettings.Development.json` connection string if needed.
3. From solution root, run the API project:

```powershell
dotnet run --project backend\src\SPILSalesOrder.API
```

Note: You may need to add the projects to a solution and run `dotnet restore` to pull package dependencies.
