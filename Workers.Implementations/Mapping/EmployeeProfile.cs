using AutoMapper;
using Workers.Abstractions.Dtos;
using Workers.DAL.Models;

namespace Workers.Implementations.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, Employee>();
        CreateMap<EmployeeDto, Employee>()
            .ReverseMap();
    }
}