using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Regions Region { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public virtual List<Regions> Region { get; set; }
    }

    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<City> Cities { get; set; }
    }

    public class Regions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual City City { get; set; }
    }
}
