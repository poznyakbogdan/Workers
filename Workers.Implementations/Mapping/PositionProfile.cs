using AutoMapper;
using Workers.Abstractions.Dtos;
using Workers.DAL.Models;

namespace Workers.Implementations.Mapping;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<PositionDto, Position>()
            .ReverseMap();
    }
}