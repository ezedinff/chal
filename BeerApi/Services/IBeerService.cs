using BeerApi.DTOs;

namespace BeerApi.Services;

public interface IBeerService
{
    Task<IEnumerable<BeerDto>> GetAllBeersAsync();
    Task<BeerDto?> GetBeerByIdAsync(int id);
    Task<IEnumerable<BeerDto>> SearchBeersAsync(string query);
    Task<BeerDto> CreateBeerAsync(CreateBeerDto createBeerDto);
    Task<BeerDto?> AddRatingAsync(int id, AddRatingDto ratingDto);
} 