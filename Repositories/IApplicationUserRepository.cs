using System.Collections.Generic;
using GreenHealth.Models;
using GreenHealth.ViewModels;

namespace GreenHealth.Repositories
{
    public interface IApplicationUserRepository
    {
        List<UserViewModel> GetUsers();
        ApplicationUser GetUser(string id);
    }
}