using FloodManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRoles> roleManager)
        {
            context.Database.EnsureCreated();
            var adminRole = new ApplicationRoles { Name = "Global Administrator", Discription = "Global Admniistrator with every Access" };
            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(new ApplicationRoles { Name = "National Administrator", Discription = "National ADmniistrator with every Access" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "State Co-Ordinator", Discription = "State ADmniistrator with every Access" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "District Co-Ordinator", Discription = "District ADmniistrator with every Access" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "Regional Co-Ordinator", Discription = "Regional ADmniistrator with every Access" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "Distributer", Discription = "Main ADmniistrator with every Access" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "Members", Discription = "Main ADmniistrator with every Access" });
            var adminUser = new ApplicationUser { UserName = "Administrator", Email = "admin@gmail.com" };
            var result = await userManager.CreateAsync(adminUser);
            if (result.Succeeded)
            {
                await userManager.AddPasswordAsync(adminUser, "Abc123@");
                await userManager.AddToRolesAsync(adminUser, new[] { "Global Administrator",
                    "National Administrator",
                    "State Co-Ordinator",
                    "District Co-Ordinator",
                    "Distributer",
                    "Members",
                });
            }
        }
    }
}
