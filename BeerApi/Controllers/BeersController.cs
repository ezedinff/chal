using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeerApi.Data;
using BeerApi.DTOs;
using BeerApi.Models;

namespace BeerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeersController : ControllerBase
{
    private readonly BeerDbContext _context;

    public BeersController(BeerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BeerDto>>> GetBeers()
    {
        return await _context.Beers
            .Select(b => new BeerDto(b.Id, b.Name, b.Type, b.AverageRating))
            .ToListAsync();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<BeerDto>>> SearchBeers([FromQuery] string query)
    {
        return await _context.Beers
            .Where(b => b.Name.Contains(query))
            .Select(b => new BeerDto(b.Id, b.Name, b.Type, b.AverageRating))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<BeerDto>> CreateBeer(CreateBeerDto createBeerDto)
    {
        var beer = new Beer
        {
            Name = createBeerDto.Name,
            Type = createBeerDto.Type
        };

        _context.Beers.Add(beer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetBeer),
            new { id = beer.Id },
            new BeerDto(beer.Id, beer.Name, beer.Type, beer.AverageRating)
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BeerDto>> GetBeer(int id)
    {
        var beer = await _context.Beers.FindAsync(id);

        if (beer == null)
        {
            return NotFound();
        }

        return new BeerDto(beer.Id, beer.Name, beer.Type, beer.AverageRating);
    }

    [HttpPost("{id}/ratings")]
    public async Task<ActionResult<BeerDto>> AddRating(int id, AddRatingDto ratingDto)
    {
        if (ratingDto.Rating < 1 || ratingDto.Rating > 5)
        {
            return BadRequest("Rating must be between 1 and 5");
        }

        var beer = await _context.Beers.FindAsync(id);

        if (beer == null)
        {
            return NotFound();
        }

        beer.Ratings.Add(ratingDto.Rating);
        await _context.SaveChangesAsync();

        return new BeerDto(beer.Id, beer.Name, beer.Type, beer.AverageRating);
    }
} 