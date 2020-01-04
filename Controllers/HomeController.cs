using System;
using GreenHealth.Persistence;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GreenHealth.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using GreenHealth.Persistence.Repositories;
using GreenHealth.Repositories;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace GreenHealth.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private IProfile _profile;
        public HomeController(IProfile profile)
        {
            _context = new AppDbContext();
            _profile = profile;
        }

        public IActionResult Me()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var userTypes = _profile.GetUserType(userId);
            
            if(userTypes.usertype == UserTypes.Doctor)
            {
                HttpContext.Session.SetString("USERID", JsonConvert.SerializeObject(userTypes));
                return Redirect("/doctors/doctorprofile");
            }
            else if(userTypes.usertype == UserTypes.Patients)
            {
                HttpContext.Session.SetString("USERID", JsonConvert.SerializeObject(userTypes));
                return Redirect("/account/index");
            }
            else
            {
                HttpContext.Session.SetString("USERID", JsonConvert.SerializeObject(userTypes));
                return Redirect("/doctors/index");
            }
            
        }

        public async Task<IActionResult> Index()
        {
           
            return View();
        }



        //#region Dashboard Statistics
        //public IActionResult TotalPatients()
        //{
        //    var patients = _context.Patients.ToList();
        //    return Json(patients.Count());
        //}

        //public ActionResult TotalAppointments()
        //{
        //    var appointments =_context.Appointments.ToList();
        //    return Json(appointments.Count());
        //}

        //public IActionResult TotalDoctors()
        //{
        //    var doctors = _context.Doctors.ToList();
        //    return Json(doctors.Count());
        //}

        //public IActionResult TotalUsers()
        //{
        //    var users=_context.Users.ToList();
        //    return Json(users.Count());
        //}

        ////Today's patients
        //public IActionResult TodaysPatients()
        //{
        //    DateTime today = DateTime.Now.Date;
        //    var patients = _context.Patients.Where(p => p.DateTime >= today).ToList();
        //    return Json(patients.Count());
        //}
        ////Todays appointments
        //public IActionResult TodaysAppointments()
        //{
        //    DateTime today = DateTime.Now.Date;
        //    var appointments =_context.Appointments
        //        .Where(a => a.StartDateTime >= today)
        //        .ToList();
        //    return Json(appointments.Count());
        //}
        ////Available doctors
        //public IActionResult AvailableDoctors()
        //{
        //    var doctors=_context.Doctors
        //        .Where(d => d.IsAvailable)
        //        .ToList();
        //    return Json(doctors.Count());
        //}
        ////Active Accounts
        //public IActionResult ActiveAccounts()
        //{
        //    var users =_context.Users
        //        .Where(u => u.EmailConfirmed == true)
        //        .ToList();
        //    return Json(users.Count());
        //}
        
        //#endregion



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}