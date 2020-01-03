using GreenHealth.Models;
using GreenHealth.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GreenHealth.Persistence.Repositories
{
    public class PatientStatusRepository : IPatientStatusRepository
    {
        private readonly AppDbContext _context;

        public PatientStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PatientStatus> GetStatuses()
        {
            return _context.PatientStatus.ToList();
        }
    }
}