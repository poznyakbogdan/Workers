using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Workers.Abstractions;
using Workers.Abstractions.Dtos;
using Workers.DAL.Models;
using AppContext = Workers.DAL.AppContext;

namespace Workers.Implementations;

public class PositionsService : IPositionsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Position> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PositionsService> _logger;

    public PositionsService(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        ILogger<PositionsService> logger)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<Position>();
        _mapper = mapper;
        _logger = logger;
    }
    public (bool, IEnumerable<int>) Exists(IEnumerable<int> positionIds)
    {
        var existingIds = _repository.AsQueryable()
            .Where(x => positionIds.Contains(x.Id)).Select(x => x.Id);
        var notExist = positionIds.Except(existingIds).ToList();
        return (!notExist.Any(), notExist);
    }

    public async Task<IEnumerable<PositionDto>> GetMany(IEnumerable<int> ids)
    {
        var entities = await _repository.AsQueryable().AsNoTracking().Where(x => ids.Contains(x.Id)).ToListAsync();
        return _mapper.Map<IEnumerable<PositionDto>>(entities);
    }

    public async Task<PositionDto> GetById(int id)
    {
        var position = await _repository.GetByIdAsync(id);
        return _mapper.Map<PositionDto>(position);
    }

    public async Task<PositionDto> Create(PositionDto positionDto)
    {
        var entity = _mapper.Map<Position>(positionDto);
        await _repository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<PositionDto>(entity);
    }

    public async Task<PositionDto> Update(int id, PositionDto positionDto)
    {
        var entity = await _repository.GetByIdAsync(id);
        positionDto.Id = id;
        _mapper.Map(positionDto, entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<PositionDto>(entity);
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        _repository.Remove(entity);
        
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