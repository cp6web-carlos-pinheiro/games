using SampleApp.BackEnd.Dtos;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetName";

    private static readonly List<GameDto> games = [
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

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", () => games);   

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find( game => game.Id == id);

            return game is not null ? Results.Ok(game) : Results.NotFound();

        })
        .WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
            
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
        var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

        games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
        );

        return Results.NoContent();

        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });  
    }
}