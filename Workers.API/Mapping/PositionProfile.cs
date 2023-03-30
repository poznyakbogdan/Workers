using AutoMapper;
using Workers.Abstractions.Dtos;
using Workers.API.Models.Input;
using Workers.API.Models.Output;

namespace Workers.API.Mapping;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<PostPosition, PositionDto>();
        CreateMap<PutPosition, PositionDto>();
        CreateMap<PositionDto, Position>();
    }
}