using System;
using System.Collections.Generic;
using System.Text;
using FloodManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FloodManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRoles,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
