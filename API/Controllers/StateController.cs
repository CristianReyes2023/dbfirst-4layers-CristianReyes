using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class StateController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StateController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<StateDto>>> Get()
    {
        var results = await _unitOfWork.States.GetAllAsync();
        return _mapper.Map<List<StateDto>>(results);
    }
    
    [HttpGet("{id}")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StateDto>> Get(int id)
    {
        var result = await _unitOfWork.States.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<StateDto>(result);
    }
    [HttpPost] // 2611
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StateDto>> Post(StateDto resultDto)
    {
        var result = _mapper.Map<State>(resultDto);
        _unitOfWork.States.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
        {
            return BadRequest();
        }
        resultDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id}")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StateDto>> Put(int id, [FromBody] StateDto resultDto)
    {
        var exists = await _unitOfWork.States.GetByIdAsync(id);
        if (exists == null)
        {
            return NotFound();
        }
        if (resultDto.Id == 0)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return BadRequest();
        }
        // Update the properties of the existing entity with values from resultDto
        _mapper.Map(resultDto, exists);
        // if (resultDto.FechaCreacion == DateOnly.MinValue)
        // {
        //     exists.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        // }
        // The context is already tracking result, so no need to attach it
        await _unitOfWork.SaveAsync();
        // Return the updated entity
        return _mapper.Map<StateDto>(exists);
    }
    [HttpDelete("{id}")] // 2611
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.States.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.States.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
