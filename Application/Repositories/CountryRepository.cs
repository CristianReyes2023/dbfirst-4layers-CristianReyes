using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories;
public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    private readonly ejemplodb4layersContext _context;

    public CountryRepository(ejemplodb4layersContext context) : base(context)
    {
        _context = context;
    }
}
