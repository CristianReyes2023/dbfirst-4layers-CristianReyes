using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repositories;
public class CityRepository : GenericRepository<City>, ICityRepository
{
    private readonly ejemplodb4layersContext _context;

    public CityRepository(ejemplodb4layersContext context) : base(context)
    {
        _context = context;
    }
    public async Task<City> GetCustomersByCity(string city)
    {
        return await _context.Cities.Where( x => x.Name.Trim().ToLower() == city.Trim().ToLower())
        .Include(x=>x.Customers)
        .FirstAsync();
    }
}
