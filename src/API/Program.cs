using Api;
using BuildingBlocks;
using BuildingBlocks.Middlewares;
using BuildingBlocks.Modules;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.ReadFrom.Configuration(ctx.Configuration);
});

var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddBuildingBlocksServices()
    .AddMessaging()
    .RegisterModulesServices(configuration);


var app = builder.Build();


app.UseHttpsRedirection();
app.UseCorrelationMiddleware();
app.UseExceptionHandler(x => { });
app.ExposeModulesEndpoints();

app.Run();


//Todo:
//Domain notifications outbox resolver
//Structuring logging serilog
//New modules - Reservations, Room Service - Creating employee inside these modules also
//Share context between modules

