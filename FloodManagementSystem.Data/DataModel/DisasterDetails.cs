using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class DisasterDetails
    {
        public DisasterDetails()
        {
            EffectedCities = new HashSet<EffectedCities>();
            ResourceRequest = new HashSet<ResourceRequest>();
        }

        public int Id { get; set; }
        public int DisasterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Disaster Disaster { get; set; }
        public virtual ICollection<EffectedCities> EffectedCities { get; set; }
        public virtual ICollection<ResourceRequest> ResourceRequest { get; set; }
    }
}
