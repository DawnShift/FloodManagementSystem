using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class AspNetRoleClaims : BaseEntity
    {
         public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetRoles Role { get; set; }
    }
}
