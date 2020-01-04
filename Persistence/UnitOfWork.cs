using GreenHealth;
using GreenHealth.Repositories;
using GreenHealth.Models;
using GreenHealth.Persistence.Repositories;

namespace GreenHealth
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IPatientRepository Patients { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IAttendanceRepository Attandences { get; private set; }
        public ICityRepository Cities { get; private set; }
        public IDoctorRepository Doctors { get; private set; }
        public ISpecializationRepository Specializations { get; private set; }
        public IPatientStatusRepository PatientStatus { get; private set; }
        public IApplicationUserRepository Users { get; private set; }
        public IProfile Profile { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Patients = new PatientRepository(context);
            Appointments = new AppointmentRepository(context);
            Attandences = new AttendanceRepository(context);
            Cities = new CityRepository(context);
            Doctors = new DoctorRepository(context);
            Specializations = new SpecializationRepository(context);
            PatientStatus = new PatientStatusRepository(context);
            Users = new ApplicationUserRepository(context);
            Profile = new ProfileRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}