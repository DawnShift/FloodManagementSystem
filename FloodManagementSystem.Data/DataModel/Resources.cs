using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class Resources
    {
        public Resources()
        {
            CityAudit = new HashSet<CityAudit>();
            CityRequests = new HashSet<CityRequests>();
            DistributerRequests = new HashSet<DistributerRequests>();
            ResourceCollection = new HashSet<ResourceCollection>();
            StateAudit = new HashSet<StateAudit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CityAudit> CityAudit { get; set; }
        public virtual ICollection<CityRequests> CityRequests { get; set; }
        public virtual ICollection<DistributerRequests> DistributerRequests { get; set; }
        public virtual ICollection<ResourceCollection> ResourceCollection { get; set; }
        public virtual ICollection<StateAudit> StateAudit { get; set; }
    }
}
