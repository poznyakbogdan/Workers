using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Workers.Abstractions;
using Workers.Abstractions.Dtos;
using Workers.API.Extensions;
using Workers.API.Models.Input;
using Workers.API.Models.Output;

namespace Workers.API.Controllers.v1;

[ApiController]
[Route("v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesService _employeesService;
    private readonly IPositionsService _positionsService;
    private readonly IMapper _mapper;
    private readonly IValidator<PostEmployee> _postEmployeeValidator;
    private readonly IValidator<PutEmployee> _putEmployeeValidator;
    

    public EmployeesController(
        IEmployeesService employeesService, 
        IPositionsService positionsService, 
        IMapper mapper,
        IValidator<PostEmployee> postEmployeeValidator,
        IValidator<PutEmployee> putEmployeeValidator)
    {
        _employeesService = employeesService;
        _positionsService = positionsService;
        _mapper = mapper;
        _postEmployeeValidator = postEmployeeValidator;
        _putEmployeeValidator = putEmployeeValidator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployee([FromRoute]int id)
    {
        var dto = await _employeesService.GetById(id);
        if (dto is null) return NotFound();
        
        return Ok(_mapper.Map<Employee>(dto));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostEmployee([FromBody] PostEmployee postEmployee)
    {
        var validationResult = await _postEmployeeValidator.ValidateAsync(postEmployee);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        var dto = _mapper.Map<EmployeeDto>(postEmployee);
        var employee = await _employeesService.Create(dto);
        return Ok(_mapper.Map<Employee>(employee));
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] PutEmployee putEmployee)
    {
        var validationResult = await _putEmployeeValidator.ValidateAsync(putEmployee);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }
        
        var employee = await _employeesService.GetById(id);
        if (employee is null) return NotFound();
        
        var dto = _mapper.Map<EmployeeDto>(putEmployee);
        var updated = await _employeesService.Update(id, dto);
        return Ok(_mapper.Map<Employee>(updated));
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
    {
        var employee = await _employeesService.GetById(id);
        if (employee is null) return NotFound();
        
        var ok = await _employeesService.Delete(id);
        return ok ? Ok() : BadRequest();
    }
}