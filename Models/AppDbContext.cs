using GreenHealth.Persistence.EntityConfigurations;
using GreenHealth.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GreenHealth.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PatientStatus> PatientStatus { get; set; }
        //public DbSet<ApplicationRole> Roles { get; set; }
        public AppDbContext()
        {
            //var siteSettings = this.GetInfrastructure().GetRequiredService<IOptionsSnapshot<SiteSettings>>();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
             : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
            modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new PatientStatusConfiguration());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
