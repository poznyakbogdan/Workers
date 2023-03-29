using Microsoft.AspNetCore.Mvc;
using Workers.API.Models.Input;
using Workers.API.Models.Output;

namespace Workers.API.Controllers.v1;

[ApiController]
[Route("v1/[controller]")]
public class EmployeesController : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployee([FromRoute]int id)
    {
        return Ok(new Employee());
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostEmployee([FromBody] PostEmployee postEmployee)
    {
        return Ok(new Employee());
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] PutEmployee putEmployee)
    {
        return Ok(new Employee());
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
    {
        return Ok();
    }
}