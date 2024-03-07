using BuildingBlocks;
using BuildingBlocks.Modules;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddBuildingBlocksServices()
    .RegisterModulesServices(configuration);


var app = builder.Build();

app.UseHttpsRedirection();
app.ExposeModulesEndpoints();

app.Run();


//Todo:
// Validation decorator
// Middleware
// Simple logs
