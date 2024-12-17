using System.ComponentModel.DataAnnotations;

namespace BeerApi.DTOs;

public record BeerDto(int Id, string Name, string Type, double? AverageRating);

public record CreateBeerDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public required string Type { get; set; }
}

public record AddRatingDto
{
    [Required]
    [Range(1, 5)]
    public required int Rating { get; set; }
} 