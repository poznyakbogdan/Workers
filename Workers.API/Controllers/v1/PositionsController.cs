using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Workers.Abstractions;
using Workers.Abstractions.Dtos;
using Workers.API.Extensions;
using Workers.API.Models.Input;
using Workers.API.Models.Output;

namespace Workers.API.Controllers.v1;

[Route("v1/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly IPositionsService _positionsService;
    private readonly IValidator<PostPosition> _validator;
    private readonly IValidator<PutPosition> _putValidator;
    private readonly IMapper _mapper;

    public PositionsController(
        IPositionsService positionsService, 
        IValidator<PostPosition> validator,
        IValidator<PutPosition> putValidator,
        IMapper mapper)
    {
        _positionsService = positionsService;
        _validator = validator;
        _putValidator = putValidator;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Position), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPosition([FromRoute] int id)
    {
        var position = await _positionsService.GetById(id);
        if (position is null) return NotFound();

        return Ok(_mapper.Map<Position>(position));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Position), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostPosition([FromBody] PostPosition postPosition)
    {
        var validationResult = await _validator.ValidateAsync(postPosition);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        var position = await _positionsService.Create(_mapper.Map<PositionDto>(postPosition));

        return Ok(_mapper.Map<Position>(position));
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Position), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutPosition([FromRoute] int id, [FromBody] PutPosition putPosition)
    {
        var validationResult = await _putValidator.ValidateAsync(putPosition);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }
        
        var dto = _mapper.Map<PositionDto>(putPosition);
        var updated = await _positionsService.Update(id, dto);
        return Ok(_mapper.Map<Position>(updated));
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePosition([FromRoute] int id)
    {
        var position = await _positionsService.GetById(id);

        if (position == null) return NotFound();
        
        var isDeleted = await _positionsService.Delete(id);
        if (!isDeleted) return BadRequest();

        return Ok();
    }
}