using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class ResourceStatus:BaseEntity
    {
        public ResourceStatus()
        {
            ResourceCollection = new HashSet<ResourceCollection>();
        }
         
        public string Name { get; set; }

        public virtual ResourceRequest ResourceRequest { get; set; }
        public virtual ICollection<ResourceCollection> ResourceCollection { get; set; }
    }
}
