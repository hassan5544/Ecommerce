using Application.Dtos.Report;
using AutoMapper;
using Domain.Entities;

namespace Application.Map;

public class MostSoldProductProfile : Profile
{
    public MostSoldProductProfile()
    {
        CreateMap<MostSoldProduct , MostSoldProductDto>();
        CreateMap<MostSoldProductDto , MostSoldProduct>();
    }
}