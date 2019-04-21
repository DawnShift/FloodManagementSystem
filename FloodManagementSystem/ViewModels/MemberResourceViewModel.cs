using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.ViewModels
{
    public class MemberResourceViewModel
    {
    }


    public class MemebrResourceCollectionListViewMModel
    {
        public MemebrResourceCollectionListViewMModel()
        {
            this.ResourceStatus = new List<ResourceStatus> {
                 new ResourceStatus{  Id=1,Name ="Requested"},
                 new ResourceStatus{  Id=2,Name ="Waiting"},
                 new ResourceStatus{  Id=3,Name ="Transferd"},
                 new ResourceStatus{  Id=4,Name ="Complete"}
            };
        }
        public int Id { get; set; }
        [Display(Name = "Total Collected")]
        public int TotalCollected { get; set; }
        [Display(Name = "Resource Status")]
        public string Status { get; set; }
        [Display(Name = "Donars Name")]
        public string DonaarsName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public List<ResourceStatus> ResourceStatus { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Status is Required.")]
        public int StatusId { get; set; }
        [Display(Name = "Resource")]
        public string Resource { get; set; }
        public int ResourceId { get; set; }
    }


    public class MemberResourceDonationListViewModel
    {
        public MemberResourceDonationListViewModel()
        {
            this.ResourceStatus = new List<ResourceStatus> {
                 new ResourceStatus{  Id=1,Name ="Requested"},
                 new ResourceStatus{  Id=2,Name ="Waiting"},
                 new ResourceStatus{  Id=3,Name ="Transferd"},
                 new ResourceStatus{  Id=4,Name ="Complete"}
            };
        }

        public int Id { get; set; }
        [Display(Name = "Total Needed")]
        public int TotalNeeded { get; set; }
        [Display(Name = "Details")]
        public string ResourceDetails { get; set; }
        [Display(Name = "Request Status")]
        public string Status { get; set; }
        [Display(Name = "Requester Name")]
        public string RequesterName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Status is Required.")]
        public int StatusId { get; set; }
        [Display(Name = "Resource")]
        public string Resource { get; set; }
        public int ResourceId { get; set; }
        public List<ResourceStatus> ResourceStatus { get; set; }
    }


    public class ResourceStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }



    public class DistributerRequestViewModel
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Resource field is required.")]
        [Display(Name = "Resource")]
        public int ResourceId { get; set; }
        public List<AddressViewModel> Resources { get; set; }
        [Display(Name ="Required Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Add atleast 1 Qty.")] 
        public int TotalNeeded { get; set; }
        public int RegionId { get; set; }
    }

    public enum ResourceStatusEnum
    {
        Requested = 1,
        Waiting = 2,
        Transferd = 3,
        Complete = 4
    }
}
