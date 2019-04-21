using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.Models
{
    public partial class EffectedCities:BaseEntity
    {
         public int Stateid { get; set; }
        public int DisasterDetailsId { get; set; }
        public bool IsActive { get; set; }

        public virtual DisasterDetails DisasterDetails { get; set; }
        public virtual State State { get; set; }
    }
}
