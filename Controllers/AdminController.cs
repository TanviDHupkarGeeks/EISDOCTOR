using GreenHealth.Models;
using GreenHealth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenHealth.Controllers
{
    public class AdminController : Controller
    {
        private AppDbContext db = new AppDbContext();
       
        public static async Task Initialize(AppDbContext context, 
                                            UserManager<ApplicationUser> userManager,
                                            RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            string admin1 = "";

            string role1 = "Admin";
            string role2 = "Doctor";
            string role3 = "Patient";
            string desc1 = "This is an Admin Role";
            string desc2 = "This is a Doctor Role";
            string desc3 = "This is a Patient Role";
            var hasher = new PasswordHasher<ApplicationUser>();

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role3) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role3, desc3, DateTime.Now));
            }

            if (await userManager.FindByEmailAsync("tanvidhupkar0728@gmail.com") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
                var user = new ApplicationUser
                {
                    Name = "Tanvi",
                    UserName = "tanvidhupkar0728@gmail.com",
                    Email = "tanvidhupkar0728@gmail.com",
                    EmailConfirmed = true,
                    Role = role1,
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "p@ssword"),
                    UserType = 0
                 
            };
               
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(user, user.Role);
                }
                admin1 = user.Id;
            }

            if (!context.Cities.Any())
            {
                context.Cities.AddRange(new City { Id = 1, Name = "Antrim", }, new City { Id = 2, Name = "Armagh", }, new City { Id = 3, Name = "Carlow", }, new City { Id = 4, Name = "Cavan", }, new City { Id = 5, Name = "Clare", },
                 new City { Id = 6, Name = "Cork" }, new City {Id = 7, Name = "Derry(Londonderry)", }, new City { Id = 8, Name = "Donegal", }, new City { Id = 9, Name = "Down", }, new City { Id = 10, Name = "Dublin", },
                 new City { Id = 11, Name = "Fermanagh", }, new City { Id = 12, Name = "Galway", }, new City { Id = 13, Name = "Kerry", }, new City { Id = 14, Name = "Kilkenny", }, new City { Id = 15, Name = "Laois(Queens)", },
                 new City { Id = 16, Name = "Limerick", }, new City { Id = 17, Name = "Longford", }, new City { Id = 18, Name = "Louth", }, new City { Id = 19, Name = "Mayo", }, new City { Id = 20, Name = "Meath", });
   
            }

            if (!context.Specializations.Any())
            {
                context.Specializations.AddRange(new Specialization { Name = "Anaesthesia", }, new Specialization { Name = "Clinical oncology", }, new Specialization { Name = "Clinical radiology", }, new Specialization { Name = "Clinical radiology", },
                new Specialization { Name = "Emergency medicine", }, new Specialization { Name = "General practice(GP)", }, new Specialization { Name = "Cardiology" });
            }
            context.SaveChanges();
        }
        //public ActionResult Index()
        //{
        //    return View(db.Admin.ToList());
        //}

        //// GET: /Administration/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AdministrationModel Administrationmodel = db.Administrations.Find(id);
        //    if (Administrationmodel == null)
        //    {
        //        return View("Error");
        //    }
        //    return View(Administrationmodel);
        //}

        //// POST: /Administration/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name,Value")] AdministrationModel Administrationmodel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(Administrationmodel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(Administrationmodel);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
