using Microsoft.EntityFrameworkCore;
using BeerApi.Models;

namespace BeerApi.Data;

public class BeerDbContext : DbContext
{
    public BeerDbContext(DbContextOptions<BeerDbContext> options) : base(options)
    {
    }

    public DbSet<Beer> Beers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>()
            .Property(b => b.Ratings)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList()
            );
    }
} 