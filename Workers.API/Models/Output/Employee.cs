namespace Workers.API.Models.Output;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SecondName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<Position> Positions { get; set; }
}