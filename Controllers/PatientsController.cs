using System;
using System.Linq;
using System.Threading.Tasks;
using GreenHealth;
using GreenHealth.Models;
using GreenHealth.Repositories;
using GreenHealth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GreenHealth.Controllers
{
    //[Authorize(Roles = RoleName.DoctorRoleName + "," + RoleName.AdministratorRoleName)]
    public class PatientsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IProfile _profile;
        private readonly IPatientRepository _patient;
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork, IProfile profile, IPatientRepository patient)
        {
            this.userManager = userManager;
            _unitOfWork = unitOfWork;
            _patient = patient;
            _profile = profile;
            this.roleManager = roleManager;
        }
        public ActionResult Index()
        {
            return View(_patient.GetPatients());
        }


        public IActionResult Details(int id)
        {
            //var patientprofile = _profile.GetPatientById(id);
            var viewModel = new PatientDetailViewModel()
            {
                Patient = _unitOfWork.Patients.GetPatient(id),
                Appointments = _unitOfWork.Appointments.GetAppointmentWithPatient(id),
                Attendances = _unitOfWork.Attandences.GetAttendance(id),
                CountAppointments = _unitOfWork.Appointments.CountAppointments(id),
                CountAttendance = _unitOfWork.Attandences.CountAttendances(id)
            };
            return View("Details", viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Patientprofile(int id)
        {
            var user = userManager.Users.First(x => x.Email == User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var viewModel = new PatientDetailViewModel()
                {
                    Patient = _unitOfWork.Patients.GetPatient(id),
                };
                return View(viewModel);
            }
        }

        [Authorize(Roles = "Patient, Admin")]
        public ActionResult Create()
        {
            var viewModel = new PatientFormViewModel
            {
                Cities = _unitOfWork.Cities.GetCities(),
                Heading = "New Patient",
                Name = User.Identity.Name
            };
            return View("PatientForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Cities = _unitOfWork.Cities.GetCities();
                return View("PatientForm", viewModel);


            }
            var user = userManager.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var patient = new Patient
            {

                Name = User.Identity.Name,
                Phone = viewModel.Phone,
                Address = viewModel.Address,
                DateTime = DateTime.Now,
                BirthDate = viewModel.GetBirthDate(),
                Height = viewModel.Height,
                Weight = viewModel.Weight,
                CityId = viewModel.City,
                Sex = viewModel.Sex,
                Token = (2018 + _unitOfWork.Patients.GetPatients().Count()).ToString().PadLeft(7, '0'),
                UserId = user.Id
            };
            //var userU = new ApplicationUser
            //{
            //    PhoneNumber = viewModel.Phone
              
            //};

            // var result = userManager.cre(userU);
           
            _unitOfWork.Patients.Add(patient);
            _unitOfWork.Complete();
            //if (User.IsInRole(RoleName.AdministratorRoleName) && user == null)
            //{
            //    var newUser = new ApplicationUser()
            //    {
            //        Name = viewModel.Name,
            //        UserName = viewModel.Email,
            //        Email = model.Email,
            //        Role = RoleName.PatientRoleName,
            //        IsActive = true
            //    };
            //}
            return RedirectToAction("Me", "Home");

            // TODO: BUG redirect to detail 
            //return RedirectToAction("Details", new { id = viewModel.Id });
        }


        public ActionResult Edit(int id)
        {
            var patient = _unitOfWork.Patients.GetPatient(id);

            var viewModel = new PatientFormViewModel
            {
                Heading = "Edit Patient",
                Id = patient.Id,
                Name = patient.Name,
                Phone = patient.Phone,
                Date = patient.DateTime,
                //Date = patient.DateTime.ToString("d MMM yyyy"),
                BirthDate = patient.BirthDate.ToString("dd/MM/yyyy"),
                Address = patient.Address,
                Height = patient.Height,
                Weight = patient.Weight,
                Sex = patient.Sex,
                City = patient.CityId,
                Cities = _unitOfWork.Cities.GetCities()
            };
            return View("PatientForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PatientFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Cities = _unitOfWork.Cities.GetCities();
                return View("PatientForm", viewModel);
            }


            var patientInDb = _unitOfWork.Patients.GetPatient(viewModel.Id);
            patientInDb.Id = viewModel.Id;
            patientInDb.Name = viewModel.Name;
            patientInDb.Phone = viewModel.Phone;
            patientInDb.BirthDate = viewModel.GetBirthDate();
            patientInDb.Address = viewModel.Address;
            patientInDb.Height = viewModel.Height;
            patientInDb.Weight = viewModel.Weight;
            patientInDb.Sex = viewModel.Sex;
            patientInDb.CityId = viewModel.City;

            _unitOfWork.Complete();
            return RedirectToAction("Index", "Patients")
;
        }



    }
}