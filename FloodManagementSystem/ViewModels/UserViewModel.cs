using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.ViewModels
{
    public class UserViewModel
    {
        public List<User> UserList { get; set; }
        public IList<string> RolesList { get; set; }

    }


    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public string Role { get; set; }
        [Display(Name="Role")]
        public int RoleId { get; set; }

    }
}
