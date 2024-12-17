using Microsoft.EntityFrameworkCore;
using BeerApi.Data;
using BeerApi.Models;

namespace BeerApi.Repositories;

public class BeerRepository : IBeerRepository
{
    private readonly BeerDbContext _context;

    public BeerRepository(BeerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Beer>> GetAllAsync()
    {
        return await _context.Beers.ToListAsync();
    }

    public async Task<Beer?> GetByIdAsync(int id)
    {
        return await _context.Beers.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Beer>> SearchAsync(string query)
    {
        return await _context.Beers
            .Where(b => b.Name.Contains(query))
            .ToListAsync();
    }

    public async Task<Beer> CreateAsync(Beer beer)
    {
        _context.Beers.Add(beer);
        await SaveChangesAsync();
        return beer;
    }

    public async Task<Beer?> AddRatingAsync(int id, int rating)
    {
        var beer = await GetByIdAsync(id);
        if (beer == null) return null;

        beer.AddRating(rating);
        await SaveChangesAsync();
        return beer;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
} 