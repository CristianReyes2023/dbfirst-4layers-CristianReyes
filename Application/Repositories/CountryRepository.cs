using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repositories;
public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    private readonly ejemplodb4layersContext _context;

    public CountryRepository(ejemplodb4layersContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Country> GetCountryByNameStates(string country)
    {
        return await _context.Countries.Where(x => x.Name.Trim().ToLower() == country.Trim().ToLower())
        .Include(x=>x.States)
        .ThenInclude(x=>x.Cities)
        .FirstAsync();
    }
    public async Task<Country> GetCountryByName(string country)
    {
        return await _context.Countries.Where( x => x.Name.Trim().ToLower() == country.Trim().ToLower()).FirstAsync();
    }
}
