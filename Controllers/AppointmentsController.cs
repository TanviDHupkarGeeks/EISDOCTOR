using System;
using System.Linq;
using System.Net;
using GreenHealth;
using GreenHealth.Models;
using GreenHealth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GreenHealth.Controllers
{
    public class AppointmentsController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentsController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            
            var user = _userManager.Users.FirstOrDefault(x => x.Email == User.Identity.Name );
         
            if (User.IsInRole(RoleName.AdministratorRoleName))
            {
                var appointments = _unitOfWork.Appointments.GetAppointments();
                return View(appointments);
            }
            //else if (User.IsInRole(RoleName.DoctorRoleName))
            //{
              

            //    return RedirectToAction("Details", "Doctors", new { id = user.Id });
            //}
            else
            {
                var appointments = _unitOfWork.Appointments.GetAppointmentsById(user.Id);


                if (appointments != null)
                {
                    return View(appointments);
                }
                else
                {
                    return View("You do not have any appointment at the moment");
                }
            }
           
        }

        public ActionResult Details(int id)
        {
            var appointment = _unitOfWork.Appointments.GetAppointmentWithPatient(id);
            return View("_AppointmentPartial", appointment);
        }
        //public ActionResult Patients(int id)
        //{
        //    var viewModel = new DoctorDetailViewModel()
        //    {
        //        Appointments = _unitOfWork.Appointments.GetAppointmentByDoctor(id),
        //    };
        //    //var upcomingAppnts = _unitOfWork.Appointments.GetAppointmentByDoctor(id);
        //    return View(viewModel);
        //}
        [Authorize (Roles = "Patient") ]
        public ActionResult Create()
        {
            var userId = HttpContext.Session.GetString("USERID");
            var sessionModel = JsonConvert.DeserializeObject<UserViewModel>(userId);
            if (sessionModel != null)
            {
                var viewModel = new AppointmentFormViewModel
                {
                    Patient = sessionModel.ClientId,
                    Doctors = _unitOfWork.Doctors.GetAvailableDoctors(),

                    Heading = "New Appointment"
                };
                return View(viewModel);
            }
            else
            {
                //change this to redirect to the login screen becasuse the only reason is that you are not authorized
                return Redirect("Index");
            }          
        }


        public ActionResult BookDoctor(int id)
        {
            if(id == null)
            {
                return RedirectToAction("index", "Doctors");
            }
            else
            {
                var existingDoctor = _unitOfWork.Doctors.GetDoctor(id);
                if(existingDoctor != null)
                {
                    ViewBag.DoctorName = existingDoctor.Name;
                    var userId = HttpContext.Session.GetString("USERID");
                    var sessionModel = JsonConvert.DeserializeObject<UserViewModel>(userId);
                    if (sessionModel != null)
                    {
                        var viewModel = new AppointmentFormViewModel
                        {
                            Patient = sessionModel.ClientId,
                            Doctor = id,
                            Heading = "New Appointment"
                        };
                        return View("Create", viewModel);
                    }
                }
                
                return RedirectToAction("index", "Doctors");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentFormViewModel viewModel)
        {
         
            if (viewModel.GetStartDateTime() < DateTime.Now.AddDays(-1))
            {
                viewModel.Doctors = _unitOfWork.Doctors.GetAvailableDoctors();
                return View(viewModel);

            }
            var appointment = new Appointment()
            {
                StartDateTime = viewModel.GetStartDateTime(),
                Detail = viewModel.Detail,
                Status = true,
                PatientId = viewModel.Patient,
                DoctorId = viewModel.Doctor

            };
            //Check if the slot is available
            if (_unitOfWork.Appointments.ValidateAppointment(appointment.StartDateTime, viewModel.Doctor))
                return View("InvalidAppointment");

            _unitOfWork.Appointments.Add(appointment);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Appointments");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var appointment = _unitOfWork.Appointments.GetAppointment(id);
            var viewModel = new AppointmentFormViewModel()
            {
                Heading = "New Appointment",
                Id = appointment.Id,
                Date = appointment.StartDateTime.ToString("dd/MM/yyyy"),
                Time = appointment.StartDateTime.ToString("HH:mm"),
                Detail = appointment.Detail,
                Status = appointment.Status,
                Patient = appointment.PatientId,
                Doctor = appointment.DoctorId,
                //Patients = _unitOfWork.Patients.GetPatients(),
                Doctors = _unitOfWork.Doctors.GetDoctors()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Doctors = _unitOfWork.Doctors.GetDoctors();
                viewModel.Patients = _unitOfWork.Patients.GetPatients();
                return View(viewModel);
            }
            //if (viewModel.GetStartDateTime() < DateTime.Now.AddDays(-1))
            //{
            //    viewModel.Doctors = _unitOfWork.Doctors.GetAvailableDoctors();
            //    return View(viewModel);

            //}

            var appointmentInDb = _unitOfWork.Appointments.GetAppointment(viewModel.Id);
            //appointmentInDb.Id = viewModel.Id;
            //appointmentInDb.StartDateTime = viewModel.GetStartDateTime();
            //appointmentInDb.Detail = viewModel.Detail;
            appointmentInDb.Status = viewModel.Status;
            //appointmentInDb.PatientId = viewModel.Patient;
            //appointmentInDb.DoctorId = viewModel.Doctor;
          
            if (!_unitOfWork.Appointments.ValidateAppointment(appointmentInDb.StartDateTime, viewModel.Doctor))
                return View("InvalidAppointment");

            _unitOfWork.Appointments.Add(appointmentInDb);
            _unitOfWork.Complete();
            return RedirectToAction("Index");

        }

        public ActionResult DoctorsList()
        {
            var doctors = _unitOfWork.Doctors.GetAvailableDoctors();
            bool isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return Json(doctors.ToArray());
            return RedirectToAction("Create");
        }

        public PartialViewResult GetUpcommingAppointments(int id)
        {
            var appointments = _unitOfWork.Appointments.GetTodaysAppointments(id);
            return PartialView(appointments);
        }

    }
}