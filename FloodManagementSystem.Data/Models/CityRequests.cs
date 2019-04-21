using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class CityRequests:BaseEntity
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int TotalNeeded { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Resources Resource { get; set; }
    }
}
