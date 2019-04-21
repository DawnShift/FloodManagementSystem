using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class City : BaseEntity
    {
        public City()
        {
            CityAudit = new HashSet<CityAudit>();
            CityRequests = new HashSet<CityRequests>();
            Regions = new HashSet<Regions>();
            ResourceAudit = new HashSet<ResourceAudit>();
        }

         public string Name { get; set; }
        public int? StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<CityAudit> CityAudit { get; set; }
        public virtual ICollection<CityRequests> CityRequests { get; set; }
        public virtual ICollection<Regions> Regions { get; set; }
        public virtual ICollection<ResourceAudit> ResourceAudit { get; set; }
    }
}
