using System.Collections.Generic;
using System.Linq;
using GreenHealth.Dto;
using GreenHealth.Models;
using GreenHealth.Repositories;
using GreenHealth.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GreenHealth.Persistence.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly AppDbContext _context;
     

        public ApplicationUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<UserViewModel> GetUsers()
        {
            return (from user in _context.Users
                    from IUserRoleStore in _context.UserRoles
                    join role in _context.Roles
                        on user.Id equals role.Id
                    select new UserViewModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Role = role.Name,
                    }).ToList();

        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.Find(id);
        }

    }
}