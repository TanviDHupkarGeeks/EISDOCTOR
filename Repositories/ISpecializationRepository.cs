using System.Collections.Generic;
using GreenHealth.Models;

namespace GreenHealth.Repositories
{
    public interface ISpecializationRepository
    {
        IEnumerable<Specialization> GetSpecializations();
    }
}
