using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FloodManagementSystem.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class StateViewModel
    {
        public AddressViewModel States { get; set; }
        public CityViewModel City { get; set; }
        public RegionViewModel Region { get; set; }
    }

    public class CityViewModel
    {
        public int Id { get; set; }

        public List<AddressViewModel> State { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "State field is required.")]
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Required] 
        public string City { get; set; }
        public List<AddressViewModel> Regions { get; set; }
    }

    public class RegionViewModel
    {
        public int Id { get; set; }

        public List<AddressViewModel> City { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "City field is required.")]
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Required] 
        public string Region { get; set; } 
    }
}
