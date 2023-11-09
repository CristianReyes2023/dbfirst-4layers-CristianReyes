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
public class CountryController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CountryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CountryDto>>> Get()
    {
        var results = await _unitOfWork.Countries.GetAllAsync();
        return _mapper.Map<List<CountryDto>>(results);
    }

    [HttpGet("{id}")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDto>> Get(int id)
    {
        var result = await _unitOfWork.Countries.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<CountryDto>(result);
    }

    [HttpGet("getCountryByName/{Name}")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDto>> GetCountryByName(string name)
    {
        var result = await _unitOfWork.Countries.GetCountryByName(name);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<CountryDto>(result);
    }
    [HttpPost] // 2611
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CountryDto>> Post(CountryDto resultDto)
    {
        var result = _mapper.Map<Country>(resultDto);
        _unitOfWork.Countries.Add(result);
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
    public async Task<ActionResult<CountryDto>> Put(int id, [FromBody] CountryDto resultDto)
    {
        var exists = await _unitOfWork.Countries.GetByIdAsync(id);
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
        return _mapper.Map<CountryDto>(exists);
    }
    [HttpDelete("{id}")] // 2611
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Countries.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Countries.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
