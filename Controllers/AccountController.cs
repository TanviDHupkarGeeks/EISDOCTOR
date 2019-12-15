using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GreenHealth.Models;
using GreenHealth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace GreenHealth.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IUnitOfWork _unitOfWork;

        //public AccountController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        public AccountController(UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signInManager,
                                    RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _unitOfWork = unitOfWork;

        }

        // GET: Account
        [Authorize]
        public IActionResult Index()
        {

            return View();
        }

        // GET: Account/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl)
        {
            RegisterViewModel model = new RegisterViewModel
            {
                //ReturnUrl = returnUrl,
                //ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

            };

            return View(model);
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    Role = RoleName.PatientRoleName,
                    IsActive = false


                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //var message = new MimeMessage();
                    //message.From.Add(new MailboxAddress("Tanvi", "tanvidhupkar@gmail.com"));
                    ////From Address
                    //message.To.Add(new MailboxAddress(user.Name, user.Email));
                    ////To Address
                    //message.Subject = "Welcome to Green Health";

                    //message.Body = new TextPart("plain")
                    //{
                    //    Text = " You are welcome to Green Health. Please feel free to explore the community and get an appointment with the doctor of your choice today"
                    //};


                    await userManager.AddToRoleAsync(user, RoleName.PatientRoleName);
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, model.Name));
                    await signInManager.SignInAsync(user, isPersistent: false);
                    //using (var client = new SmtpClient())
                    //{
                    //    //client.Connect("smtp.gmail.com", 587, false);
                    //    //client.Authenticate("chrisokhamera@gmail.com", "sophia123456");
                    //    //client.Send(message);
                    //    //client.Disconnect(true);
                    //}
                   
                        return RedirectToAction("create", "patients");
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);


        }

        // GET: Account/Login
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

            };

            return View(model);
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false).ConfigureAwait(true);

                if (result.Succeeded)
                {
                    
                    return RedirectToAction("index", "account");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }

            return View(model);




        }

        //Doctor registration
        [AllowAnonymous]
        public IActionResult RegisterDoctor()
        {
            var viewModel = new DoctorFormViewModel()
            {
                Specializations = _unitOfWork.Specializations.GetSpecializations()
                // Doctors = _doctorRepository.GetDectors()
            };
            return View("DoctorForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterDoctor(DoctorFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Name = viewModel.RegisterViewModel.Name,
                    UserName = viewModel.RegisterViewModel.Email,
                    Email = viewModel.RegisterViewModel.Email,
                    IsActive = true
                };
                var result = await userManager.CreateAsync(user, viewModel.RegisterViewModel.Password);

                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, RoleName.DoctorRoleName);


                    Doctor doctor = new Doctor()
                    {
                        Name = viewModel.Name,
                        Phone = viewModel.Phone,
                        Address = viewModel.Address,
                        IsAvailable = true,
                        SpecializationId = viewModel.Specialization,
                        PhysicianId = user.Id,

                    };
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, doctor.Name));
                    //Mapper.Map<DoctorFormViewModel, Doctor>(model, doctor);

                    _unitOfWork.Doctors.Add(doctor);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index", "Doctors");
                }

                ModelState.AddModelError("", "Something failed.");
                return View(viewModel);
            }

            viewModel.Specializations = _unitOfWork.Specializations.GetSpecializations();

            // If we got this far, something failed, redisplay form
            return View("DoctorForm", viewModel);
        }


        //External Login Code
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                 new { ReturnUrl = returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        //ExternalLoginCallback
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins =
                        (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState
                    .AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            // Get the login information about the user from the external login provider
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState
                    .AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {

                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await userManager.CreateAsync(user);

                    }

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on support@Greenhealth.com";

                return View("Error");
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Method to Log User Out
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction("index", "home");
        }
    }
}