using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class Regions : BaseEntity
    {
        public Regions()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            DistributerRequests = new HashSet<DistributerRequests>();
            ResourceAudit = new HashSet<ResourceAudit>();
            ResourceCollection = new HashSet<ResourceCollection>();
            ResourceRequest = new HashSet<ResourceRequest>();
        }
         
        public string Name { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<DistributerRequests> DistributerRequests { get; set; }
        public virtual ICollection<ResourceAudit> ResourceAudit { get; set; }
        public virtual ICollection<ResourceCollection> ResourceCollection { get; set; }
        public virtual ICollection<ResourceRequest> ResourceRequest { get; set; }
    }
}
