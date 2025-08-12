# Real Estate Backend (ASP.NET Core 6)

## Requirements
- .NET 6 SDK
- SQL Server (LocalDB works) or change to PostgreSQL in Program.cs
- dotnet-ef for migrations

## Setup
1. Open the `backend` folder.
2. Restore packages: `dotnet restore`
3. Add migrations: `dotnet ef migrations add Init`
4. Update database: `dotnet ef database update`
5. Run: `dotnet run`

Endpoints:
- POST /auth/register
- POST /auth/login
- GET /properties
- GET /properties/{id}
- POST /favorites/{propertyId} (auth)
- GET /favorites (auth)
