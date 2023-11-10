using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos;
public class StateListCityDto
{
    public string Name { get; set; }
    public List<CityDto> Cities { get;set;}
}
