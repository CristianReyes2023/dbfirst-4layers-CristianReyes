using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country> GetCountryByName(string name);
    Task<Country> GetCountryByNameStates(string name);
}
