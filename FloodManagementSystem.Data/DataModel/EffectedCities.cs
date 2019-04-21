using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class EffectedCities
    {
        public int Id { get; set; }
        public int Stateid { get; set; }
        public int DisasterDetailsId { get; set; }
        public bool IsActive { get; set; }

        public virtual DisasterDetails DisasterDetails { get; set; }
        public virtual State State { get; set; }
    }
}
