using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class ResourceRequest
    {
        public int Id { get; set; }
        public string RequestDetails { get; set; }
        public int ResourceId { get; set; }
        public int TotalNeeded { get; set; }
        public int RegionId { get; set; }
        public int DisasterDetailsId { get; set; }
        public int ResourceStatus { get; set; }
        public string UserId { get; set; }

        public virtual DisasterDetails DisasterDetails { get; set; }
        public virtual ResourceStatus IdNavigation { get; set; }
        public virtual Regions Region { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
