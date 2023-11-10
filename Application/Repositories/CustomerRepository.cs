using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories;
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ejemplodb4layersContext _context;

    public CustomerRepository(ejemplodb4layersContext context) : base(context)
    {
        _context = context;
    }
    
}
