using System.Collections.Generic;
using System.Linq;
using GreenHealth.Dto;
using GreenHealth.Models;
using GreenHealth.Repositories;

namespace GreenHealth.Persistence.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.ToList();
        }
    }
}