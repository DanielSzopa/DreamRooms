using Api;
using BuildingBlocks;
using BuildingBlocks.Logging.Enrichers;
using BuildingBlocks.Middlewares;
using BuildingBlocks.Modules;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.Enrich.FromLogContext();
    cfg.Enrich.With(new RemoveUnnecessaryPropertiesEnricher());
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
app.UseLoggingEnricherMiddleware();
app.UseExceptionHandler(x => { });

app.ExposeModulesEndpoints();

app.Run();


//Todo:
//Create seperate migrations table per Module
//Add reservations repository
//New modules - Reservations, Room Service - Creating employee inside these modules also

