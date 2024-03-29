﻿using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class AspNetUserRoles : BaseEntity
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
