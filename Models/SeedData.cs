using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenHealth.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
           
            AppDbContext context = new AppDbContext();
            
            context.Database.Migrate();
            if (!context.Users.Any())
            {
//                string role1 = "Admin";
//                string role2 = "Doctor";
//                string role3 = "Patient";
//                string desc1 = "This is an Admin Role";
//                string desc2 = "This is a Doctor Role";
//                string desc3 = "This is a Patient Role";
//                string password = "P@ssword";
//                context.Users.AddRange(
//                new ApplicationUser
//                {
//                    Name = "Tanvi",
//                    UserName = "tanvidhupkar0728@gmail.com",
//                    Email = "tanvidhupkar0728@gmail.com",
//                    PasswordHash = password,
//                    EmailConfirmed = true,
//                    Role = role1,
//                    IsActive = true
//                });

//// I need to put same value into db
                
//                context.Roles.AddRange(new ApplicationRole
//                {
//                    Name = role1,
//                    Description = desc1,
//                    CreationDate = DateTime.Now

//                },
//                 new ApplicationRole
//                 {
//                     Name = role1,
//                     Description = desc1,
//                     CreationDate = DateTime.Now

//                 },
//                 new ApplicationRole
//                 {
//                     Name = role1,
//                     Description = desc1,
//                     CreationDate = DateTime.Now

//                 });
                context.Cities.AddRange(
                new City { Name = "Antrim", }, new City { Name = "Armagh", }, new City { Name = "Carlow", }, new City { Name = "Cavan", }, new City { Name = "Clare", },
                new City { Name = "Cork", }, new City { Name = "Derry(Londonderry)", }, new City { Name = "Donegal", }, new City { Name = "Down", }, new City { Name = "Dublin", },
                new City { Name = "Fermanagh", }, new City { Name = "Galway", }, new City { Name = "Kerry", }, new City { Name = "Kilkenny", }, new City { Name = "Laois(Queens)", },
                new City { Name = "Limerick", }, new City { Name = "Longford", }, new City { Name = "Louth", }, new City { Name = "Mayo", }, new City { Name = "Meath", });
                context.Specializations.AddRange(
                new Specialization { Name = "Anaesthesia", }, new Specialization { Name = "Clinical oncology", }, new Specialization { Name = "Clinical radiology", }, new Specialization { Name = "Clinical radiology", },
                new Specialization { Name = "Emergency medicine", }, new Specialization { Name = "General practice(GP)", }, new Specialization { Name = "Cardiology" });

               
                context.SaveChanges();
            }
        }
    }
}
