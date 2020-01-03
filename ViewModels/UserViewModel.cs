using GreenHealth.Models;

namespace GreenHealth.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        //public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool? IsActive { get; set; }
        public UserTypes usertype { get; set; }

        public  int  ClientId { get; set; }

    }
}