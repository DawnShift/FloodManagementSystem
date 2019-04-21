using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class DistributerRequests
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int TotalNeeded { get; set; }
        public int RegionId { get; set; }

        public virtual Regions Region { get; set; }
        public virtual Resources Resource { get; set; }
    }
}
