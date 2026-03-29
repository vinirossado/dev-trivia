# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build

# Run API (Swagger at http://localhost:5288/swagger)
cd DevTrivia.API && dotnet run

# Run tests
cd DevTrivia.Tests && dotnet test

# EF Core migrations
dotnet ef migrations add <MigrationName> --project DevTrivia.API
dotnet ef database update --project DevTrivia.API
```

Docker (PostgreSQL + API):
```bash
docker-compose -f compose.yaml up
```

## Architecture

**Stack:** .NET 10, ASP.NET Core, EF Core + PostgreSQL, JWT (HS256), BCrypt, FluentValidation, Swashbuckle

**Pattern:** Capability-based (feature-sliced) layered architecture:

```
Controller → Service → Repository → EF Core → PostgreSQL
```

Each domain feature lives in `DevTrivia.API/Capabilities/{Feature}/` with this internal layout:
- `Controllers/` — HTTP endpoints
- `Services/` — business logic (interface + implementation)
- `Repositories/` — data access (interface + implementation)
- `Database/Entities/` — EF Core entity models (inherit `BaseEntity`)
- `Database/EntityTypeConfiguration/` — Fluent API config
- `Models/` — request/response DTOs
- `Validators/` — FluentValidation validators
- `Extensions/` — mapping extension methods (entity ↔ DTO)

**Shared infrastructure:**
- `Capabilities/Shared/Repositories/BaseRepository<T>` — generic CRUD base (all repos inherit this)
- `Capabilities/Shared/Database/Entities/BaseEntity` — adds `Id`, `CreatedAt`, `UpdatedAt`
- `Infrastructure/Logging/LogMessages.cs` — compile-time structured logging (event IDs 1000–5999)
- `Infrastructure/Filters/` — global FluentValidation filter wired in `Program.cs`
- `Infrastructure/Swagger/` — JWT Bearer support in Swagger UI

**Implemented capabilities:** User (auth + CRUD), Category, Question

**Planned (not yet implemented):** AnswerOptions, Matches, PlayerAnswers, PlayerStats, AuthSessions

## Key Conventions

- All API responses use `ApiResponse<T>(bool Success, T? Data, string? Message, IEnumerable<string>? Errors)`.
- DTOs and entities are strictly separated; mapping is done via extension methods (e.g., `QuestionMappingExtensions`).
- JWT secret must be 32+ characters; clock skew is disabled.
- Passwords use BCrypt with work factor 12.
- Log event IDs: 1000–1999 auth, 2000–2999 user CRUD, 3000–3999 JWT, 4000–4999 DB errors, 5000–5999 validation.

## Configuration

Required local config (not committed) in `appsettings.Development.json`:
```json
{
  "ConnectionStrings": { "DefaultConnection": "Host=...;Database=...;Username=...;Password=..." },
  "JwtSettings": {
    "SecretKey": "<32+ char key>",
    "Issuer": "DevTrivia",
    "Audience": "DevTriviaUsers",
    "ExpirationInMinutes": 60
  }
}
```

Azure Key Vault (`KeyVault:Vault`) is optional; `Postgres--TriviaConnectionString` overrides the connection string when present.
