using System.ComponentModel.DataAnnotations;

namespace Workers.DAL.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(64)]
    public string FirstName { get; set; }
    
    [MaxLength(64)]
    public string SecondName { get; set; }
    
    [MaxLength(64)]
    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }
    
    public List<Position> Positions { get; set; }
}