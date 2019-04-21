using System.Collections.Generic;
using System.Linq;

namespace FloodManagementSystem.Data
{
    public static class UserRoles
    {

        public static List<RolesList> Roles { get { return GetRoles(); } }


        private static List<RolesList> GetRoles()
        {
            return new List<RolesList> {
                new RolesList { Id= 1, Role ="Members" },
         new RolesList{ Id =2,Role="Distributer" },
                new RolesList{ Id=3, Role="District Co-Ordinator" },
                new RolesList{ Id=4,Role = "State Co-Ordinator"},
                new RolesList{  Id=5,Role="Administrator"}
                   };
        }

        public static int GetRoleId(string roleName)
        {
            return GetRoles().Where(x => x.Role == roleName).FirstOrDefault().Id;
        }
    }

    public class RolesList
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }


    public enum ResourceStatus
    {
        Requested = 1,
        Waiting = 2,
        Transfered = 3,
        Complete = 4
    }
}
