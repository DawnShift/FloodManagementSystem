using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class ResourceAudit
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int TotalCountAvailable { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }

        public virtual City City { get; set; }
        public virtual Regions Region { get; set; }
        public virtual State State { get; set; }
    }
}
