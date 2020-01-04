using System.Collections.Generic;
using GreenHealth.Models;

namespace GreenHealth.Repositories
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities();
    }
}