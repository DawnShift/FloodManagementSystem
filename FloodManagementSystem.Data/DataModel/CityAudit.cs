using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class CityAudit
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int TotalAvailable { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Resources Resource { get; set; }
    }
}
