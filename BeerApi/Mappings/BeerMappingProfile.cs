using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;

namespace BeerApi.Mappings;

public class BeerMappingProfile : Profile
{
    public BeerMappingProfile()
    {
        CreateMap<Beer, BeerDto>()
            .ForCtorParam("AverageRating", 
                opt => opt.MapFrom(src => src.AverageRating));
        CreateMap<CreateBeerDto, Beer>();
    }
} 