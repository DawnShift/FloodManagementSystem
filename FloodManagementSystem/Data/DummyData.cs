using FloodManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FloodManagementSystem.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRoles> roleManager)
        {
            context.Database.EnsureCreated();
           await roleManager.CreateAsync(new ApplicationRoles { Name = "Administrator", Discription = "Administrator" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "State Co-Ordinator", Discription = "State Co-Ordinator" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "District Co-Ordinator", Discription = "District Co-Ordinator" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "Regional Co-Ordinator", Discription = "Regional Co-Ordinator" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "Distributer", Discription = "Distributer" });
            await roleManager.CreateAsync(new ApplicationRoles { Name = "Members", Discription = "Members" });
            var adminUser = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
            var result = await userManager.CreateAsync(adminUser);
            if (result.Succeeded)
            {
                await userManager.AddPasswordAsync(adminUser, "Abc123@");
                await userManager.AddToRolesAsync(adminUser, new[] { "Administrator",
                    "State Co-Ordinator",
                    "District Co-Ordinator",
                    "Distributer",
                    "Members",
                });
            }
        }
    }
}
