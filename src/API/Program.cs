using BuildingBlocks;
using BuildingBlocks.Modules;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddBuildingBlocksServices()
    .RegisterModulesServices();


var app = builder.Build();

app.UseHttpsRedirection();
app.ExposeModulesEndpoints();

app.Run();