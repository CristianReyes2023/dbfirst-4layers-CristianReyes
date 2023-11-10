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
public class CityController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CityController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CityDto>>> Get()
    {
        var results = await _unitOfWork.Cities.GetAllAsync();
        return _mapper.Map<List<CityDto>>(results);
    }
    
    [HttpGet("{id}")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDto>> Get(int id)
    {
        var result = await _unitOfWork.Cities.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<CityDto>(result);
    }
    [HttpGet("GetCustomerByCity/{name}")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDto>> GetCustomersByCity(string name)
    {
        var result = await _unitOfWork.Cities.GetCustomersByCity(name);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<CityDto>(result);
    }
    [HttpPost] // 2611
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CityDto>> Post(CityDto resultDto)
    {
        var result = _mapper.Map<City>(resultDto);
        _unitOfWork.Cities.Add(result);
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
    public async Task<ActionResult<CityDto>> Put(int id, [FromBody] CityDto resultDto)
    {
        var exists = await _unitOfWork.Cities.GetByIdAsync(id);
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
        return _mapper.Map<CityDto>(exists);
    }
    [HttpDelete("{id}")] // 2611
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Cities.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Cities.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
