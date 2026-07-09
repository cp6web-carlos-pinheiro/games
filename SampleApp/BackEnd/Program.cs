using Microsoft.AspNetCore.OpenApi;
using SampleApp.BackEnd.Dtos;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    // current workaround for port forwarding in codespaces
    // https://github.com/dotnet/aspnetcore/issues/57332
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Servers = [];
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

List<GameDto> games = [
    new (
        1, 
        "The Legend of Zelda: Breath of the Wild", 
        "Action-adventure", 
        59.99m, 
        new DateOnly(2017, 3, 3)),
    new (
        2, 
        "Super Mario Odyssey", 
        "Platform", 
        55.99m, 
        new DateOnly(2017, 10, 27)),
    new (
        3, 
        "Red Dead Redemption 2", 
        "Action-adventure", 
        57.99m, 
        new DateOnly(2017, 10, 27)),
];

app.MapGet("/games", () => games);   

app.MapGet("/teste", () => "OK");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
