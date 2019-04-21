using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.ViewModels
{
    public class StateListViewModel
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string Resource { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public int Total { get; set; }
    }

    public class InventoryViewModel
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Resource field is required.")]
        [Display(Name = "Resource")]
        public int ResourceId { get; set; }
        public List<AddressViewModel> Resources { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please add atleast 1 Qty.")]
        public int Total { get; set; }
    }
}
