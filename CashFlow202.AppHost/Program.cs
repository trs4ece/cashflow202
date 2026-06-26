var builder = DistributedApplication.CreateBuilder(args);

// CashFlow202.Web is the ASP.NET Core host that serves the Blazor WASM app locally.
// For production, publish CashFlow202.Web.Client standalone to Azure Static Web Apps.
builder.AddProject("cashflow202-web",
    Path.Combine("..", "CashFlow202.Web.Server", "CashFlow202.Web.csproj"));

builder.Build().Run();
