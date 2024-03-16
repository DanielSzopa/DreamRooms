using Api;
using BuildingBlocks;
using BuildingBlocks.Modules;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddBuildingBlocksServices()
    .AddMessageBroker()
    .RegisterModulesServices(configuration);


var app = builder.Build();

app.UseHttpsRedirection();
app.UseExceptionHandler(x => { });
app.ExposeModulesEndpoints();

app.Run();


//Todo:
//Domain notifications
//Outbox, inbox
//Add CorellationId (Middleware), adding it to logs
//New modules - Reservations, Room Service - Creating employee inside these modules also
//Add context which help to keep tracibility between modules

