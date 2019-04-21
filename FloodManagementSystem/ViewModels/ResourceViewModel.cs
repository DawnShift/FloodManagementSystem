using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FloodManagementSystem.ViewModels
{
    public class ResourceModel
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Resource field is required.")]
        [Display(Name = "Resource")]
        public int ResourceId { get; set; }
        [Display(Name = "Place")]
        [Range(1, int.MaxValue, ErrorMessage = "Place field is required.")]
        public int RegionId { get; set; }
        public List<AddressViewModel> EffectedRegion { get; set; }
        [Display(Name = "Total")]
        public int Count { get; set; }
        public List<Resources> Resource { get; set; }
        public int DisasterId { get; set; }

        [Display(Name ="Description")]
        public string RequestDetails { get; set; }
    }

    public class EffectedPlaces
    {
        public PlaceModel Places { get; set; }
    }


    public class PlaceModel
    {
        public AddressViewModel State { get; set; }
        public List<CityViewModel> Cities { get; set; }
    }

    public class ResourceViewModel
    {
        public Resources Resources { get; set; }
        public Professonals Professonals { get; set; }
    }

    public class Resources
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Item Name is Required.")]
        [Display(Name = "Item")]
        public string Name { get; set; }
    }

    public class Professonals

    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Professional occupation is Required.")]
        [Display(Name = "Occupation")]
        public string Name { get; set; }
    }
}
