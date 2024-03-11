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
app.UseExceptionHandler(x => { });
app.ExposeModulesEndpoints();

app.Run();


//Todo:
//Logs - Maybe first without serilog
//New modules - Reservations, Room Service - Creating entity inside these modules also
//Add context

