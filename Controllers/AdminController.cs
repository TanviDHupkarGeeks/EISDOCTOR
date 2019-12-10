using GreenHealth.Models;
using GreenHealth.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenHealth.Controllers
{
    public class AdminController
    {
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
                    UserName = "tanvidhupkar0728@gmail.com",
                    Email = "tanvidhupkar0728@gmail.com",
                    FirstName = "Tanvi",
                    LastName = "Dhupkar",
                    PhoneNumber = "08000777799",
                    EmailConfirmed = true

                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                admin1 = user.Id;
            }
        }
    }
}
