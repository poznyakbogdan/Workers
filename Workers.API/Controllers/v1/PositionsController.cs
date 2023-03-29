using Microsoft.AspNetCore.Mvc;
using Workers.API.Models.Input;
using Workers.API.Models.Output;

namespace Workers.API.Controllers.v1;

[Route("v1/[controller]")]
public class PositionsController : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Position), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPosition()
    {
        return Ok(new Position());
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Position), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostPosition([FromBody] PostPosition postPosition)
    {
        return Ok(new Position());
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Position), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutPosition([FromRoute ]int id, [FromBody] PutPosition putPosition)
    {
        return Ok(new Position());
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Position), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePosition([FromRoute ]int id)
    {
        return Ok(new Position());
    }
}