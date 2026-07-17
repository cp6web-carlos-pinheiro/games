using BackEnd.Data;
using BackEnd.Endpoints;
using BackEnd.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiConfiguration();

builder.Services.AddValidation();

var connString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.ConfigureOpenApi();

app.UseHttpsRedirection();

app.MapWeatherForecastEndpoints();

app.MapGamesEndpoints();

app.Run();