namespace Workers.Abstractions.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string SecondName { get; set; }
    
    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }
 
    public List<int> PositionsId { get; set; }
    public List<PositionDto> Positions { get; set; }
}