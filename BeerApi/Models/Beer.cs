using System.Collections.Generic;

namespace BeerApi.Models;

public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<int> Ratings { get; set; } = new();
    
    public double? AverageRating => Ratings.Count > 0 ? Ratings.Average() : null;
} 