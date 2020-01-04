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
            string password = "P@ssword";

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
                 
            };
                await userManager.AddPasswordAsync(user, password);
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(user, user.Role);
                }
                admin1 = user.Id;
            }
            
            //if (!context.Cities.Any())
            //{
            //   context.Cities.AddRange(new City { Name = "Antrim", }, new City { Name = "Armagh", }, new City { Name = "Carlow", }, new City { Name = "Cavan", }, new City { Name = "Clare", },
            //    new City { Name = "Cork", }, new City { Name = "Derry(Londonderry)", }, new City { Name = "Donegal", }, new City { Name = "Down", }, new City { Name = "Dublin", },
            //    new City { Name = "Fermanagh", }, new City { Name = "Galway", }, new City { Name = "Kerry", }, new City { Name = "Kilkenny", }, new City { Name = "Laois(Queens)", },
            //    new City { Name = "Limerick", }, new City { Name = "Longford", }, new City { Name = "Louth", }, new City { Name = "Mayo", }, new City { Name = "Meath", });

            //    context.SaveChanges();
            //}
            
            //if (!context.Specializations.Any())
            //{
            //    context.Specializations.AddRange(new Specialization { Name = "Anaesthesia", }, new Specialization { Name = "Clinical oncology", }, new Specialization { Name = "Clinical radiology", }, new Specialization { Name = "Clinical radiology", },
            //    new Specialization { Name = "Emergency medicine", }, new Specialization { Name = "General practice(GP)", }, new Specialization { Name = "Cardiology" });
            //}
            //context.SaveChanges();
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
