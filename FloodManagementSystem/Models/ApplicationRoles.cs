
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.Models
{
    public class ApplicationRoles : IdentityRole
    {
        public ApplicationRoles() : base()
        {
        }

        public ApplicationRoles(string roleName, string description) : base(roleName)
        {
            this.Discription = description;
        }
        public string Discription { get; set; }
    }
}
