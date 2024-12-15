namespace BeerApi.DTOs;

public record BeerDto(int Id, string Name, string Type, double? AverageRating);

public record CreateBeerDto
{
    public required string Name { get; set; }
    public required string Type { get; set; }
}

public record AddRatingDto
{
    public required int Rating { get; set; }
} 