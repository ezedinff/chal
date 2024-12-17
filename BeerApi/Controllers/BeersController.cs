using Microsoft.AspNetCore.Mvc;
using BeerApi.DTOs;
using BeerApi.Services;

namespace BeerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeersController : ControllerBase
{
    private readonly IBeerService _beerService;

    public BeersController(IBeerService beerService)
    {
        _beerService = beerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BeerDto>>> GetBeers()
    {
        return Ok(await _beerService.GetAllBeersAsync());
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<BeerDto>>> SearchBeers([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }

        return Ok(await _beerService.SearchBeersAsync(query));
    }

    [HttpPost]
    public async Task<ActionResult<BeerDto>> CreateBeer(CreateBeerDto createBeerDto)
    {
        var beer = await _beerService.CreateBeerAsync(createBeerDto);
        return CreatedAtAction(nameof(GetBeer), new { id = beer.Id }, beer);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BeerDto>> GetBeer(int id)
    {
        var beer = await _beerService.GetBeerByIdAsync(id);
        if (beer == null) return NotFound();
        
        return Ok(beer);
    }

    [HttpPost("{id}/ratings")]
    public async Task<ActionResult<BeerDto>> AddRating(int id, AddRatingDto ratingDto)
    {
        var beer = await _beerService.AddRatingAsync(id, ratingDto);
        if (beer == null) return NotFound();

        return Ok(beer);
    }
} 