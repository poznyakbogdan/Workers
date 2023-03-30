using Workers.Abstractions.Dtos;

namespace Workers.Abstractions;

public interface IEmployeesService
{
    public Task<EmployeeDto> GetById(int id);
    
    public Task<EmployeeDto> Create(EmployeeDto employeeDto);
    
    public Task<EmployeeDto> Update(int id, EmployeeDto employeeDto);

    public Task<bool> Delete(int id);
}