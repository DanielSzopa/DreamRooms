using BuildingBlocks;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddBuildingBlocksServices();


var app = builder.Build();

app.UseHttpsRedirection();

app.Run();