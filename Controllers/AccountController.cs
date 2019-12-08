 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenHealth.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenHealth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManger;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManger,
                                    SignInManager<IdentityUser> signInManager)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    var result = await userManger.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index","home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Create
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                   
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, 
                        model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        
                        return RedirectToAction("index", "home");
                    }
                  
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                
                }

                return RedirectToAction(nameof(Login));

            }
            catch
            {
                return View();
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
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}