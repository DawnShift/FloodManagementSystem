using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class Disaster
    {
        public Disaster()
        {
            DisasterDetails = new HashSet<DisasterDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<DisasterDetails> DisasterDetails { get; set; }
    }
}
