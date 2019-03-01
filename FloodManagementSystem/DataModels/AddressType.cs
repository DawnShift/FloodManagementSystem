using System;
using System.Collections.Generic;

namespace FloodManagementSystem.DataModels
{
    public partial class AddressType
    {
        public AddressType()
        {
            Address = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
