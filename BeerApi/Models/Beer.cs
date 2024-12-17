using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BeerApi.Models;

public class Beer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    private readonly List<int> _ratings = new();
    public IReadOnlyCollection<int> Ratings => _ratings.AsReadOnly();
    
    public double? AverageRating => _ratings.Any() ? Math.Round(_ratings.Average(), 1) : null;

    public void AddRating(int rating)
    {
        _ratings.Add(rating);
    }
} 