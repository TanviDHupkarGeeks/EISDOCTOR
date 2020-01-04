using System.Collections.Generic;
using GreenHealth.Models;

namespace GreenHealth.Repositories
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetDoctors();
        IEnumerable<Doctor> GetAvailableDoctors();
        Doctor GetDoctor(int id);
        Doctor GetProfile(string userId);
        void Add(Doctor doctor);
    }
}