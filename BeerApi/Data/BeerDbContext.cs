using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BeerApi.Models;

namespace BeerApi.Data;

public class BeerDbContext : DbContext
{
    public BeerDbContext(DbContextOptions<BeerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Beer> Beers => Set<Beer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.Property(b => b.Name)
                .IsRequired();

            entity.Property(b => b.Type)
                .IsRequired();

            entity.Ignore(b => b.AverageRating);
            
            var converter = new ValueConverter<List<int>, string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList()
            );

            var comparer = new ValueComparer<List<int>>(
                (l1, l2) => l1!.SequenceEqual(l2!),
                l => l.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                l => l.ToList()
            );

            entity.Property<List<int>>("_ratings")
                .HasColumnName("Ratings")
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);
        });
    }
} 