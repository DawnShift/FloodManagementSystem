using System;
using System.Collections.Generic;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
            EffectedCities = new HashSet<EffectedCities>();
            ResourceAudit = new HashSet<ResourceAudit>();
            StateAudit = new HashSet<StateAudit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<EffectedCities> EffectedCities { get; set; }
        public virtual ICollection<ResourceAudit> ResourceAudit { get; set; }
        public virtual ICollection<StateAudit> StateAudit { get; set; }
    }
}
