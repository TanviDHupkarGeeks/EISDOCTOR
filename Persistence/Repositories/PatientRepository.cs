using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using GreenHealth.Models;
using GreenHealth.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreenHealth.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Patient> GetPatients()
        {
            return _context.Patients.Include(c => c.Cities);
        }

        /// <summary>
        /// /Get single patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Patient GetPatient(int id)
        {
            return _context.Patients
                .Include(c => c.Cities)
                .SingleOrDefault(p => p.Id == id);
            //return _context.Patients.Find(id);
        }
        /// <summary>
        /// Get newly added patients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Patient> GetRecentPatients()
        {
            return _context.Patients
                .Where(a => DateTime.Now.Subtract(a.DateTime).Days == 0)
                .Include(c => c.Cities);
        }



        /// <summary>
        /// Add new patient
        /// </summary>
        /// <param name="patient"></param>
        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
        }
        /// <summary>
        /// Delete existing patient
        /// </summary>
        /// <param name="patient"></param>
        public void Remove(Patient patient)
        {
            _context.Patients.Remove(patient);
        }
    }
}