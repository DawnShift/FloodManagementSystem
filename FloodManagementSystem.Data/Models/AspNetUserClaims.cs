using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class AspNetUserClaims : BaseEntity
    {
         public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
