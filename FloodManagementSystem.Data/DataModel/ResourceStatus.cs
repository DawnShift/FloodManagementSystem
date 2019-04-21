using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class ResourceStatus
    {
        public ResourceStatus()
        {
            ResourceCollection = new HashSet<ResourceCollection>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ResourceRequest ResourceRequest { get; set; }
        public virtual ICollection<ResourceCollection> ResourceCollection { get; set; }
    }
}
