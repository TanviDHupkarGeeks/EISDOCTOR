using GreenHealth.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GreenHealth.Infrastructure
{
    public class HospitalDbContext : IdentityDbContext
    {
        public HospitalDbContext() : base("DefaultConnection")
        {
            //Empty constructor.
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<AdministrationModel> Administrations { get; set; }
    }
}