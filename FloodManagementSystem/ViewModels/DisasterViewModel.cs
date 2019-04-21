using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FloodManagementSystem.ViewModels
{
    public class DisasterViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public IFormFile File { get; set; }
    }


    public class DisasterDetailsviewModel
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Disaster field is required.")]
        [Display(Name = "Disaster")]
        public int DisasterId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "States Effected field is required.")]
        [Display(Name = "States Effected")]
        public int[] EffectedStates { get; set; }
        public List<AddressViewModel> States { get; set; }
        public List<AddressViewModel> Disasters { get; set; }
        public string ImgURL { get; set; }
        // [Required]
        //  [Range(1, int.MaxValue, ErrorMessage = "States Effected field is required.")]
        //[Display(Name = "States Effected")]
        //public string EffecedAreaString { get; set; }
    }

}
