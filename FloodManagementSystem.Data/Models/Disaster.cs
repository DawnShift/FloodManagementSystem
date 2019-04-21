using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class Disaster : BaseEntity
    {
        public Disaster()
        {
            DisasterDetails = new HashSet<DisasterDetails>();
        }

         public string Name { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<DisasterDetails> DisasterDetails { get; set; }
    }
}
