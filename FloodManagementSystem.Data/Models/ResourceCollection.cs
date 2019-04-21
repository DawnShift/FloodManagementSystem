using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class ResourceCollection:BaseEntity
    {
         public int ResourceId { get; set; }
        public int TotalCollected { get; set; }
        public int RgionId { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }

        public virtual Resources Resource { get; set; }
        public virtual Regions Rgion { get; set; }
        public virtual ResourceStatus StatusNavigation { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
