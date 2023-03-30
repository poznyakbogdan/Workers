using Workers.Abstractions.Dtos;

namespace Workers.Abstractions;

public interface IPositionsService
{
    public (bool, IEnumerable<int>) Exists(IEnumerable<int> positionIds);

    public Task<IEnumerable<PositionDto>> GetMany(IEnumerable<int> ids);
    
    public Task<PositionDto> GetById(int id);
    
    public Task<PositionDto> Create(PositionDto positionDto);
    
    public Task<PositionDto> Update(int id, PositionDto positionDto);

    public Task<bool> Delete(int id);
}