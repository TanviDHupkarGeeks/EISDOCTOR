using System.Collections.Generic;
using GreenHealth.Models;

namespace GreenHealth.Repositories
{
    public interface IPatientStatusRepository
    {
        IEnumerable<PatientStatus> GetStatuses();

    }
}