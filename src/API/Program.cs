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
//Add Transactional outbox/inbox for integration events, also implement some error handling with dead letter storage
//Error handling for notification outbox - maybe some deadletter table
//Add more business logic

