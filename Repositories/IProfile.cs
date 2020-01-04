using GreenHealth.Models;
using GreenHealth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenHealth.Repositories
{
    public interface IProfile
    {
        Task<dynamic> GetProfile(string userId);
        Doctor GetDoctorsDetails(string userId);
        Doctor GetDoctorById(int id);
        Patient GetPatientDetails(string userId);
        Patient GetPatientById(int id);
        UserViewModel GetUserType(string userId);
    }
}
