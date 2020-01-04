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
        Patient GetPatientDetails(string userId);
        UserViewModel GetUserType(string userId);
    }
}
