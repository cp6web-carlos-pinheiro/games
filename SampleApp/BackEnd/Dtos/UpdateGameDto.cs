namespace SampleApp.BackEnd.Dtos;
public record UpdateGameDto(    
    string Name,
    string Genre,
    decimal Price,    
    DateOnly ReleaseDate       
);