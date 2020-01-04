using GreenHealth;
using GreenHealth.Repositories;
using GreenHealth.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace GreenHealth.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProfile _profile;
        public DoctorsController(IUnitOfWork unitOfWork, IProfile profile)
        {
            _unitOfWork = unitOfWork;
            _profile = profile;
        }

        public ActionResult Index()
        {
            var UserSession = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("USERID"));
            var doctorprofile = _profile.GetDoctorsDetails(UserSession.Id);
            return View(Tuple.Create(UserSession, doctorprofile));
        }

        //Details for admin
        public ActionResult Details(int id)
        {
            var viewModel = new DoctorDetailViewModel
            {
                Doctor = _unitOfWork.Doctors.GetDoctor(id),
                UpcomingAppointments = _unitOfWork.Appointments.GetTodaysAppointments(id),
                Appointments = _unitOfWork.Appointments.GetAppointmentByDoctor(id),
            };
            return View(viewModel);
        }

        public ActionResult DoctorProfile()
        {
            var userId = User.Identity.GetUserId();
            var viewModel = new DoctorDetailViewModel
            {
                Doctor = _unitOfWork.Doctors.GetProfile(userId),
                Appointments = _unitOfWork.Appointments.GetUpcommingAppointments(userId),
            };
            return View(viewModel);
        }
        public ActionResult Edit(int id)
        {
            var doctor = _unitOfWork.Doctors.GetDoctor(id);
            if (doctor == null) return StatusCode(404);
            var viewModel = new DoctorFormViewModel()
            {

                Id = doctor.Id,
                Name = doctor.Name,
                Phone = doctor.Phone,
                Address = doctor.Address,
                IsAvailable = doctor.IsAvailable,
                Specialization = doctor.SpecializationId,
                Specializations = _unitOfWork.Specializations.GetSpecializations()

            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(DoctorFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Specializations = _unitOfWork.Specializations.GetSpecializations();
                return View(viewModel);
            }

            var doctorInDb = _unitOfWork.Doctors.GetDoctor(viewModel.Id);
            doctorInDb.Id = viewModel.Id;
            doctorInDb.Name = viewModel.Name;
            doctorInDb.Phone = viewModel.Phone;
            doctorInDb.Address = viewModel.Address;
            doctorInDb.IsAvailable = viewModel.IsAvailable;
            doctorInDb.SpecializationId = viewModel.Specialization;

            _unitOfWork.Complete();

            return RedirectToAction("Details", new { id = viewModel.Id });
        }


    }

}

