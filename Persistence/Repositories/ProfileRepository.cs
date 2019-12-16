using GreenHealth.Models;
using GreenHealth.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using GreenHealth.ViewModels;

namespace GreenHealth.Persistence.Repositories
{
    public class ProfileRepository : IProfile
    {
        private readonly AppDbContext _context;
        public ProfileRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<dynamic> GetProfile(string userId)
        {
            ApplicationUser UserDetails = await _context.Users.FindAsync(userId);
            if(UserDetails.UserType == UserTypes.Doctor)
            {
                return GetDoctorsDetails(UserDetails.Id);
            }
            else
            {
                return GetPatientDetails(UserDetails.Id);
            }
        }

        public Doctor GetDoctorsDetails( string userId)
        {
            return _context.Doctors.FirstOrDefault(x => x.PhysicianId == userId);
        }

        public Patient GetPatientDetails(string userId)
        {
            return _context.Patients.FirstOrDefault(x =>x.UserId == userId);
        }

        public UserViewModel GetUserType(string userId)
        {
            var userDetails = _context.Users.Find(userId);

            return new UserViewModel
            {
                Email = userDetails.Email,
                IsActive = userDetails.IsActive,
                Role = userDetails.Role,
                Id = userDetails.Id,
                usertype = userDetails.UserType
            };
        }
    }
}
