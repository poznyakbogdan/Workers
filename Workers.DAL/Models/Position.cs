using System.ComponentModel.DataAnnotations;

namespace Workers.DAL.Models;

public class Position
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(256)]
    public string Title { get; set; }
    
    public int Grade { get; set; }
    
    public List<Employee> Employees { get; set; }
}