using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;
using BeerApi.Repositories;

namespace BeerApi.Services;

public class BeerService : IBeerService
{
    private readonly IBeerRepository _repository;
    private readonly IMapper _mapper;

    public BeerService(IBeerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BeerDto>> GetAllBeersAsync()
    {
        var beers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<BeerDto>>(beers);
    }

    public async Task<BeerDto?> GetBeerByIdAsync(int id)
    {
        var beer = await _repository.GetByIdAsync(id);
        return beer == null ? null : _mapper.Map<BeerDto>(beer);
    }

    public async Task<IEnumerable<BeerDto>> SearchBeersAsync(string query)
    {
        var beers = await _repository.SearchAsync(query);
        return _mapper.Map<IEnumerable<BeerDto>>(beers);
    }

    public async Task<BeerDto> CreateBeerAsync(CreateBeerDto createBeerDto)
    {
        var beer = _mapper.Map<Beer>(createBeerDto);
        var createdBeer = await _repository.CreateAsync(beer);
        return _mapper.Map<BeerDto>(createdBeer);
    }

    public async Task<BeerDto?> AddRatingAsync(int id, AddRatingDto ratingDto)
    {
        var beer = await _repository.AddRatingAsync(id, ratingDto.Rating);
        return beer == null ? null : _mapper.Map<BeerDto>(beer);
    }
} 