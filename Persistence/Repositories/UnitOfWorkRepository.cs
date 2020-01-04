//using GreenHealth.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GreenHealth.Persistence.Repositories
//{
//    public class UnitOfWorkRepository : IUnitOfWork
//    {
//        private IDoctorRepository _doctors;
//        private ISpecializationRepository _specializationProvider;
//        public UnitOfWorkRepository(ISpecializationRepository specialization, IDoctorRepository doctors)
//        {
//            this._specializationProvider = specialization;
//            this._doctors = doctors;
//        }s

//        public IPatientRepository Patients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//        public IAppointmentRepository Appointments => throw new NotImplementedException();

//        public IAttendanceRepository Attandences => throw new NotImplementedException();

//        public ICityRepository Cities => throw new NotImplementedException();

//        public IDoctorRepository Doctors { get => _doctors; set => _doctors = value; }
//        public ISpecializationRepository Specializations
//        {
//            get => _specializationProvider;
//            set => _specializationProvider = value;
//        }

//        public IPatientStatusRepository PatientStatus => throw new NotImplementedException();

//        public IApplicationUserRepository Users => throw new NotImplementedException();

//        public void Complete()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
