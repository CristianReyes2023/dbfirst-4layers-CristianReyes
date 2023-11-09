using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{

    private ICityRepository _cities;
    private ICountryRepository _countries;
    private ICustomerRepository _customers;
    private IPersonTypeRepository _persontypes;
    private IStateRepository _states;

    private readonly ejemplodb4layersContext _context;

    public UnitOfWork(ejemplodb4layersContext context)
    {
        _context = context;
    }
    public ICityRepository Cities
    {
        get
        {
            if (_cities == null)
            {
                _cities = new CityRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _cities;
        }
    }
    public ICountryRepository Countries
    {
        get
        {
            if (_countries == null)
            {
                _countries = new CountryRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _countries;
        }
    }
    public ICustomerRepository Customers
    {
        get
        {
            if (_customers == null)
            {
                _customers = new CustomerRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _customers;
        }
    }
    public IPersonTypeRepository PersonTypes
    {
        get
        {
            if (_persontypes == null)
            {
                _persontypes = new PersonTypeRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _persontypes;
        }
    }
    public IStateRepository States
    {
        get
        {
            if (_states == null)
            {
                _states = new StateRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _states;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}
