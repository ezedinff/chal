using BeerApi.Models;

namespace BeerApi.Repositories;

public interface IBeerRepository
{
    Task<IEnumerable<Beer>> GetAllAsync();
    Task<Beer?> GetByIdAsync(int id);
    Task<IEnumerable<Beer>> SearchAsync(string query);
    Task<Beer> CreateAsync(Beer beer);
    Task<Beer?> AddRatingAsync(int id, int rating);
    Task SaveChangesAsync();
} 