using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class Resources:BaseEntity
    {
        public Resources()
        {
            CityAudit = new HashSet<CityAudit>();
            CityRequests = new HashSet<CityRequests>();
            DistributerRequests = new HashSet<DistributerRequests>();
            ResourceCollection = new HashSet<ResourceCollection>();
            StateAudit = new HashSet<StateAudit>();
        }
         
        public string Name { get; set; }

        public virtual ICollection<CityAudit> CityAudit { get; set; }
        public virtual ICollection<CityRequests> CityRequests { get; set; }
        public virtual ICollection<DistributerRequests> DistributerRequests { get; set; }
        public virtual ICollection<ResourceCollection> ResourceCollection { get; set; }
        public virtual ICollection<StateAudit> StateAudit { get; set; }
    }
}
