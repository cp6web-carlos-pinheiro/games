using BackEnd.Endpoints;
using BackEnd.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiConfiguration();

var app = builder.Build();

app.ConfigureOpenApi();

app.UseHttpsRedirection();

app.MapWeatherForecastEndpoints();

app.MapGamesEndpoints();

app.Run();