using System;
using System.Collections.Generic;

namespace FloodManagementSystem.DataModels
{
    public partial class Address
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public int TypeId { get; set; }

        public virtual AddressType Type { get; set; }
    }
}
