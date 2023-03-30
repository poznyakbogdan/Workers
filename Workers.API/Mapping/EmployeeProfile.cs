using AutoMapper;
using Workers.Abstractions.Dtos;
using Workers.API.Models.Input;
using Workers.API.Models.Output;

namespace Workers.API.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<PostEmployee, EmployeeDto>();
        CreateMap<PutEmployee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
    }
}