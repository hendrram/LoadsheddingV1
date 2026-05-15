# LoadsheddingV1

A .NET 6 web application for tracking load-shedding schedules and lab power status updates.

## Portfolio Summary
This project demonstrates:
- ASP.NET Core (.NET 6) web development
- Entity Framework Core with SQL Server LocalDB
- Background hosted services for scheduled API updates
- Secure configuration practices (no hardcoded secrets)
- Basic data dashboard UI for operational monitoring

## Tech Stack
- **Framework:** ASP.NET Core (.NET 6)
- **Data Access:** Entity Framework Core
- **Database:** SQL Server LocalDB
- **UI:** Razor views + Bootstrap
- **Background Jobs:** `BackgroundService` hosted worker

## Features
- Displays upcoming load-shedding events
- Periodically fetches external schedule data from the EskomSePush API
- Stores event data in SQL Server
- Lab status management page for operational toggles

## Project Structure
- `Controllers/` - Request handlers and hosted update service
- `Models/` - EF Core models and DbContext
- `Views/` - Razor UI pages
- `Migrations/` - EF Core migration history
- `Program.cs` - App startup and dependency registration

## Getting Started
### Prerequisites
- .NET SDK 6.0+
- SQL Server LocalDB
- Visual Studio 2022/2026 or VS Code

### 1) Clone
```bash
git clone <your-repo-url>
cd LoadsheddingV1
```

### 2) Configure secrets (recommended)
Set API token via User Secrets (do not commit tokens):
```bash
dotnet user-secrets set "LoadSheddingApi:Token" "<your-token>"
```

Optional settings:
```bash
dotnet user-secrets set "LoadSheddingApi:AreaId" "capetown-15-rondebosch"
dotnet user-secrets set "LoadSheddingApi:BaseUrl" "https://developer.sepush.co.za/business/2.0/area"
```

### 3) Apply database migrations
```bash
dotnet ef database update --context LoadSheddingContext
dotnet ef database update --context LoadsheddingV1Context
```

### 4) Run
```bash
dotnet run
```

## Configuration
Configured via `appsettings.json` and user secrets:
- `ConnectionStrings:LoadsheddingV1ContextConnection`
- `ConnectionStrings:LoadSheddingContextConnection`
- `LoadSheddingApi:BaseUrl`
- `LoadSheddingApi:AreaId`
- `LoadSheddingApi:Token`

## Security Notes
- API tokens are not stored in source files.
- Local override files are excluded via `.gitignore`.
- For production, use environment variables or a secure secret store.

## Suggested Portfolio Enhancements
- Add screenshots/GIFs of Home and Labs pages
- Add unit/integration tests for update logic
- Add CI pipeline (build + test + lint)
- Add Docker support for reproducible local setup
- Add structured logging and health checks

## License
This project is licensed under the MIT License. See [LICENSE](LICENSE).
