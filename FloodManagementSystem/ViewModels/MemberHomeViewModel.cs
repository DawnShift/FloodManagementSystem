using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.ViewModels
{
    public class MemberHomeViewModel
    {
        public List<DisasterDetailsviewModel> Disasters { get; set; }
        public DisasterArea Requests { get; set; }
    }

    public class DisasterArea
    {
        public bool IsEffected { get; set; }
        public int DisasterDetailsId { get; set; }
    }
 
    public class MemberResourceListViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Resource")]
        public string ResourceType { get; set; }
        [Display(Name = "Resource Status")]
        public string Status { get; set; }
        [Display(Name = "Total Count")]
        public int Count { get; set; }
        [Display(Name ="Donated By")]
        public string DonatedBy { get; set; }
        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; }
    }
}
