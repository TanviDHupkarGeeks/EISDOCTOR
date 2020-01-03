
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GreenHealth.Models
{
    public enum UserTypes
    {
        Doctor = 1, Patients = 2
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public string Name { get; set; }
        public string Role { get; set; }

        public bool? IsActive { get; set; }
        public UserTypes UserType { get; set; } = UserTypes.Patients;
        public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
        {
            public MyUserClaimsPrincipalFactory(
                UserManager<ApplicationUser> userManager,
                IOptions<IdentityOptions> optionsAccessor)
                    : base(userManager, optionsAccessor) { }


            

            protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
            {
                var identity = await base.GenerateClaimsAsync(user);
                identity.AddClaim(new Claim("UserName", user.UserName ?? "[Click to edit profile]"));
                return identity;
            }
        }
    }
}
