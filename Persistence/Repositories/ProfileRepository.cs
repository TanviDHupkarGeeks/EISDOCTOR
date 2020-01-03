using GreenHealth.Models;
using GreenHealth.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using GreenHealth.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            if (UserDetails.UserType == UserTypes.Doctor)
            {
                return GetDoctorsDetails(UserDetails.Id);
            }
            else
            {
                return GetPatientDetails(UserDetails.Id);
            }
        }

        public Doctor GetDoctorsDetails(string userId)
        {
            return _context.Doctors.FirstOrDefault(x => x.PhysicianId == userId);
        }

        public Patient GetPatientDetails(string userId)
        {
            return _context.Patients.FirstOrDefault(x => x.UserId == userId);
        }

        //this method can be re-written for performance later because i think this is niot ideal because it is making to database call to get the usertype
        //I actually think it should be called once in the account controller immediately after logging in 
        // to set up your session with your details
        public UserViewModel GetUserType(string userId)
        {
            var userDetails = _context.Users.Find(userId);
            var DetailToSend = new UserViewModel
            {
                Email = userDetails.Email,
                IsActive = userDetails.IsActive,
                Role = userDetails.Role,
                Id = userDetails.Id,
                usertype = userDetails.UserType
            };
            if (userDetails.UserType == UserTypes.Doctor)
            {
                var details = GetDoctorsDetails(userId);
                DetailToSend.ClientId = details.Id;
                return DetailToSend;
            }
            else if (userDetails.UserType == UserTypes.Patients)
            {
                var details = GetPatientDetails(userId);
                DetailToSend.ClientId = details.Id;
                return DetailToSend;
            }
            return DetailToSend;


        }

        public Doctor GetDoctorById(int id)
        {
            return _context.Doctors.Include(x=>x.Physician).Include(x=>x.Specialization).FirstOrDefault(x=>x.Id == id);
        }
        public Patient GetPatientById(int id)
        {
            return _context.Patients.Include(x => x.Name).Include(x => x.Token).FirstOrDefault(x => x.Id == id);
        }
    }
}
