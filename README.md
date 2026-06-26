# Cashflow 202 Tracker

A mobile-first web app that acts as a digital tracking sheet for the **Cashflow 202** board game (Robert Kiyosaki). It replicates the game sheets in English and does all the math for you — automatically linking properties to your income statement.

## Features

- 📊 **Financial Statement** — Income statement + balance sheet with live cash-flow calculation
- 🏠 **Deals** — Track real estate, stocks/funds/CDs, businesses, and other passive income; all automatically linked to the income statement
- 📈 **Options Worksheet** — Track Call/Put option positions (Cashflow 202–specific), record premiums, close positions, and see realised P&L
- 🚀 **Fast Track** — Unlocks (with a glowing button) the moment passive income ≥ expenses; tracks Big Deals and dream costs on the Fast Track
- 💾 **Persistent state** — All data is saved to `localStorage`; survives refreshes and lost connections with no server needed
- 🔄 **Game reset** — One-tap reset with confirmation guard
- 📱 **Mobile-first PWA** — Installable on iOS/Android, bottom tab-bar navigation, works offline

## Architecture

```
CashFlow202.sln
├── CashFlow202.Web.Client/        ← Standalone Blazor WASM (deployed to Azure SWA)
│   ├── Models/                    ← Game data models (GameState, Profession, etc.)
│   ├── Services/                  ← LocalStorageService, GameStateService
│   ├── Pages/                     ← Setup, FinancialStatement, Deals, OptionsWorksheet, FastTrack
│   ├── Layout/                    ← MainLayout with bottom tab-bar
│   ├── Shared/                    ← EditableAmount component
│   └── wwwroot/                   ← index.html, app.css, manifest.json, service-worker.js
├── CashFlow202.Web.Server/        ← ASP.NET Core host (local dev / Aspire only)
├── CashFlow202.AppHost/           ← .NET Aspire orchestration (local dev)
└── CashFlow202.ServiceDefaults/   ← Shared Aspire service configuration
```

**Deployment target**: Azure Static Web Apps (serves `CashFlow202.Web.Client` publish output).  
**Local dev**: Run via `dotnet run --project CashFlow202.AppHost` (Aspire dashboard + hot-reload).

## Deploying to Azure Static Web Apps

1. Create an **Azure Static Web App** resource in the Azure Portal (or via Azure CLI).
2. Add the deployment token as a repository secret named `AZURE_STATIC_WEB_APPS_API_TOKEN`.
3. Push to `main` — the GitHub Actions workflow (`.github/workflows/azure-static-web-app.yml`) will build and deploy automatically.

The workflow:
- Builds `CashFlow202.Web.Client` with `dotnet publish -c Release`
- Deploys `publish/wwwroot/` to Azure SWA (pure static files — no server required)

## Running locally

```bash
# Option A — Aspire (recommended): starts the server + Aspire dashboard
dotnet run --project CashFlow202.AppHost

# Option B — standalone client dev server
dotnet run --project CashFlow202.Web.Client

# Option C — server host only
dotnet run --project CashFlow202.Web.Server
```

## Game Rules Reference

| Sheet | Description |
|-------|-------------|
| Financial Statement | Income, expenses, monthly cash flow, balance sheet |
| Deals | Small deals: real estate, stocks, businesses |
| Options Worksheet | Cashflow 202 call/put options tracking |
| Fast Track | Unlocked when passive income ≥ total expenses |
