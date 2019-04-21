using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FloodManagementSystem.ViewModels
{
    public class DistrictListViewModel
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Resource { get; set; }
        [Display(Name = "Total Requested")]
        public int TotalNeeded { get; set; }
        public int ResourceId { get; set; }
        public int RegionId { get; set; }
    }

    public class DistrictRequestListViewModel
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Resource field is required.")]
        [Display(Name = "Resource")]
        public int ResourceId { get; set; }
        public List<AddressViewModel> Resources { get; set; }
        [Display(Name = "Total")]
        [Required]
        public int TotalNeeded { get; set; }
    }

}
