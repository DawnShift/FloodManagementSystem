using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class StateAudit
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public int ResourceId { get; set; }
        public int TotalAvailable { get; set; }

        public virtual Resources Resource { get; set; }
        public virtual State State { get; set; }
    }
}
