namespace Workers.API.Models.Input;

public class PostEmployee
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<int> PositionsId { get; set; }
}