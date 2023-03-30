using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Workers.Abstractions;
using Workers.Abstractions.Dtos;
using Workers.DAL.Models;

namespace Workers.Implementations;

public class EmployeesService : IEmployeesService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Employee> _employeesRepository;
    private readonly IRepository<Position> _positionsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<EmployeesService> _logger;

    public EmployeesService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EmployeesService> logger)
    {
        _unitOfWork = unitOfWork;
        _employeesRepository = _unitOfWork.GetRepository<Employee>();
        _positionsRepository = _unitOfWork.GetRepository<Position>();
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<EmployeeDto> GetById(int id)
    {
        var employee = await _employeesRepository.AsQueryable()
            .Where(x => x.Id == id)
            .Include(x => x.Positions)
            .SingleOrDefaultAsync();

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> Create(EmployeeDto employeeDto)
    {
        var positions = _positionsRepository
            .AsQueryable()
            .Where(x => employeeDto.PositionsId.Contains(x.Id));
        var entity = _mapper.Map<Employee>(employeeDto);
        entity.Positions = new List<Position>(positions);
        await _employeesRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<EmployeeDto>(entity);
    }

    public async Task<EmployeeDto> Update(int id, EmployeeDto employeeDto)
    {
        var positions = _positionsRepository
            .AsQueryable()
            .Where(x => employeeDto.PositionsId.Contains(x.Id));

        employeeDto.Id = id;
        var newEntity = _mapper.Map<Employee>(employeeDto);
        newEntity.Positions = new List<Position>(positions);
        
        var entity = await _employeesRepository
            .AsQueryable()
            .Where(x => x.Id == id)
            .Include(x => x.Positions)
            .SingleAsync();
        
        _mapper.Map(newEntity, entity);
        await _unitOfWork.CommitAsync();
        
        return _mapper.Map<EmployeeDto>(entity);
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _employeesRepository.GetByIdAsync(id);
        _employeesRepository.Remove(entity);
        
        try
        {
            await _unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during Employee DELETE operation");
            return false;
        }

        return true;
    }
}