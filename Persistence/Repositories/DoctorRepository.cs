using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GreenHealth.Models;
using GreenHealth.Repositories;

namespace GreenHealth.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        
        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctors
                .Include(s => s.Specialization)
                .Include(u => u.Physician)
                .ToList();
        }

        /// <summary>
        /// Get the available doctors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Doctor> GetAvailableDoctors()
        {
            return _context.Doctors
                .Where(a => a.IsAvailable == true)
                .Include(s => s.Specialization)
                .Include(u => u.Physician)
                .ToList();
        }
        /// <summary>
        /// Get single Doctor - Admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Doctor GetDoctor(int id)
        {
            return _context.Doctors
                .Include(s => s.Specialization)
                .Include(u => u.Physician)
                .SingleOrDefault(d => d.Id == id);
        }

        public Doctor GetProfile(string userId)
        {
            return _context.Doctors
                .Include(s => s.Specialization)
                .Include(u => u.Physician)
                .SingleOrDefault(d => d.PhysicianId == userId);
        }
        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
        }
    }
}