var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CashFlow202_Web>("cashflow202-web");

builder.Build().Run();
