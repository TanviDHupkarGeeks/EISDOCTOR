using GreenHealth.Models;
using GreenHealth.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GreenHealth.Persistence.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        public readonly AppDbContext Context;

        public SpecializationRepository(AppDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Specialization> GetSpecializations()
        {
            return Context.Specializations.ToList();
        }
    }
}