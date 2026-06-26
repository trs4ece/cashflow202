using CashFlow202.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<GameStateService>();

await builder.Build().RunAsync();
