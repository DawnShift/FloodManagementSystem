using FloodManagementSystem.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using FloodManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FloodManagementSystem.Controllers
{
    [Authorize(Roles = "Distributer")]
    public class DistributerController : Controller
    {
        private readonly IRepository<Data.Models.Resources> resourceRepo;
        private readonly IRepository<Data.Models.ResourceCollection> resourcCollectionRepo;
        private readonly IRepository<Data.Models.ResourceRequest> resourceRequestRepo;
        private readonly IRepository<City> cityRepo;
        private readonly IRepository<Data.Models.State> stateRepo;
        private readonly IRepository<Regions> regionRepo;
        private readonly IRepository<Data.Models.EffectedCities> ecRepos;
        private readonly IRepository<Data.Models.AspNetUsers> userRepo;
        private readonly UserManager<Models.ApplicationUser> _userManager;
        private readonly IRepository<DisasterDetails> ddRepo;
        private readonly IRepository<Disaster> dRepo;
        private readonly IRepository<EffectedCities> ecRepo;
        private readonly IHostingEnvironment _environment;
        private readonly IRepository<ResourceAudit> auditRepo;
        private readonly IRepository<DistributerRequests> distributerRequestRepo;


        public DistributerController(IRepository<Data.Models.Resources> resourceRepo,
            IRepository<Data.Models.ResourceCollection> resourcCollectionRepo
            , IRepository<Data.Models.City> cityRepo, IRepository<Data.Models.State> stateRepo,
             IRepository<Data.Models.Regions> regionRepo, IRepository<Data.Models.EffectedCities> ecRepos,
              IRepository<Data.Models.AspNetUsers> userRepo,
               UserManager<Models.ApplicationUser> _userManager,
               IRepository<Data.Models.ResourceRequest> resourceRequestRepo,
               IRepository<DisasterDetails> ddRepo, IRepository<Disaster> dRep,
                IRepository<ResourceAudit> auditRepo,
               IRepository<EffectedCities> ecRepo, IHostingEnvironment _environment,
                IRepository<DistributerRequests> distributerRequestRepo
            )
        {
            this.cityRepo = cityRepo;
            this.stateRepo = stateRepo;
            this.regionRepo = regionRepo;
            this.resourcCollectionRepo = resourcCollectionRepo;
            this.resourceRepo = resourceRepo;
            this.ecRepos = ecRepos;
            this.userRepo = userRepo;
            this._userManager = _userManager;
            this.resourceRequestRepo = resourceRequestRepo;
            this.ddRepo = ddRepo;
            this.dRepo = dRep;
            this.ecRepo = ecRepo;
            this._environment = _environment;
            this.auditRepo = auditRepo;
            this.distributerRequestRepo = distributerRequestRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CollectResources()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            var resourceList = (from item in resourcCollectionRepo.FilteredGet().Where(x => x.RgionId == regionId && x.Status != 4)
                                join donar in userRepo.FilteredGet() on item.UserId equals donar.Id
                                join resource in resourceRepo.FilteredGet() on item.ResourceId equals resource.Id
                                select new MemebrResourceCollectionListViewMModel
                                {
                                    Id = item.Id,
                                    DonaarsName = donar.FirstName + " " + donar.LastName,
                                    PhoneNumber = donar.PhoneNumber,
                                    Status = GetRequestStatus(item.Status),
                                    TotalCollected = item.TotalCollected,
                                    Resource = resource.Name,
                                    ResourceId = resource.Id
                                }
                                ).ToList();
            return View(resourceList ?? new System.Collections.Generic.List<MemebrResourceCollectionListViewMModel>());
        }

        public async Task<ActionResult> DonateResource()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            var resourceList = (from item in resourceRequestRepo.FilteredGet().Where(x => x.RegionId == regionId && x.ResourceStatus != 4)
                                join requester in userRepo.FilteredGet() on item.UserId equals requester.Id
                                join resource in resourceRepo.FilteredGet() on item.ResourceId equals resource.Id
                                select new MemberResourceDonationListViewModel
                                {
                                    Id = item.Id,
                                    RequesterName = requester.FirstName + " " + requester.LastName,
                                    PhoneNumber = requester.PhoneNumber,
                                    Status = GetRequestStatus(item.ResourceStatus),
                                    TotalNeeded = item.TotalNeeded,
                                    Resource = resource.Name,
                                    ResourceId = resource.Id,
                                    ResourceDetails = item.RequestDetails
                                }
                                ).ToList();
            return View((resourceList ?? new System.Collections.Generic.List<MemberResourceDonationListViewModel>()));
        }

        public ActionResult Edit(int id)
        {
            var resourceList = (from item in resourcCollectionRepo.FilteredGet().Where(x => x.Id == id)
                                join donar in userRepo.FilteredGet() on item.UserId equals donar.Id
                                join resource in resourceRepo.FilteredGet() on item.ResourceId equals resource.Id
                                select new MemebrResourceCollectionListViewMModel
                                {
                                    Id = item.Id,
                                    DonaarsName = donar.FirstName + " " + donar.LastName,
                                    PhoneNumber = donar.PhoneNumber,
                                    Status = GetRequestStatus(item.Status),
                                    StatusId = item.Status,
                                    TotalCollected = item.TotalCollected,
                                    Resource = resource.Name,
                                    ResourceId = resource.Id
                                }).FirstOrDefault();
            return View(resourceList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MemebrResourceCollectionListViewMModel model)
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            var data = resourcCollectionRepo.FilteredGet().Where(x => x.Id == model.Id).FirstOrDefault();
            data.Status = model.StatusId;
            resourcCollectionRepo.Update(data);
            if (ResourceStatusEnum.Complete == (ResourceStatusEnum)model.StatusId)
            {
                var totalCollection = auditRepo.FilteredGet().Where(x => x.RegionId == regionId && x.ResourceId == model.ResourceId).FirstOrDefault();
                if (totalCollection == null)
                {
                    var regionalDetails = regionRepo.FilteredGet().Where(x => x.Id == regionId).Include(x => x.City).FirstOrDefault();

                    var newModel = new ResourceAudit
                    {
                        CityId = (int)regionalDetails.CityId,
                        StateId = (int)regionalDetails.City.StateId,
                        ResourceId = model.ResourceId,
                        RegionId = regionId,
                        TotalCountAvailable = model.TotalCollected
                    };
                    auditRepo.Insert(newModel);
                }
                else
                {
                    totalCollection.TotalCountAvailable = totalCollection.TotalCountAvailable + model.TotalCollected;
                    auditRepo.Update(totalCollection);
                }
            }
            return RedirectToActionPermanent("CollectResources");
        }

        public async Task<ActionResult> DonateResources(int id)
        {
            ViewBag.IsAvailable = false;
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            var resourceList = (from item in resourceRequestRepo.FilteredGet().Where(x => x.Id == id)
                                join requester in userRepo.FilteredGet() on item.UserId equals requester.Id
                                join resource in resourceRepo.FilteredGet() on item.ResourceId equals resource.Id
                                select new MemberResourceDonationListViewModel
                                {
                                    Id = item.Id,
                                    RequesterName = requester.FirstName + " " + requester.LastName,
                                    PhoneNumber = requester.PhoneNumber,
                                    Status = GetRequestStatus(item.ResourceStatus),
                                    StatusId = item.ResourceStatus,
                                    TotalNeeded = item.TotalNeeded,
                                    Resource = resource.Name,
                                    ResourceId = resource.Id,
                                    ResourceDetails = item.RequestDetails
                                }).FirstOrDefault();
            var availableResource = (auditRepo.FilteredGet().Where(x => x.RegionId == regionId && x.ResourceId == resourceList.ResourceId).FirstOrDefault() ?? new ResourceAudit());
            if (availableResource.TotalCountAvailable < resourceList.TotalNeeded)
            {
                ViewBag.IsAvailable = true;
            }
            return View(resourceList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DonateResources(MemberResourceDonationListViewModel model)
        {
            var request = resourceRequestRepo.FilteredGet().Where(x => x.Id == model.Id).FirstOrDefault();
            if (ResourceStatusEnum.Requested == (ResourceStatusEnum)request.ResourceStatus && (ResourceStatusEnum)model.StatusId != ResourceStatusEnum.Requested)
            {
                System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
                var currentUser = await _userManager.GetUserAsync(currentUserClaims);
                int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
                var availableResource = auditRepo.FilteredGet().Where(x => x.RegionId == regionId && x.ResourceId == request.ResourceId).FirstOrDefault();
                availableResource.TotalCountAvailable = availableResource.TotalCountAvailable - model.TotalNeeded;
                auditRepo.Update(availableResource);
            }
            request.ResourceStatus = model.StatusId;
            resourceRequestRepo.Update(request);
            return RedirectToActionPermanent("DonateResource");
        }

        private static string GetRequestStatus(int statusId)
        {
            ResourceStatusEnum status = (ResourceStatusEnum)statusId;
            return status.ToString();
        }


        public ActionResult RequestResources()
        {
            DistributerRequestViewModel model = new DistributerRequestViewModel();
            model.Resources = (from item in resourceRepo.FilteredGetAll()
                               select new AddressViewModel
                               {
                                   Id = item.Id,
                                   Name = item.Name
                               }
                               ).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestResources(DistributerRequestViewModel model)
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            var newRequest = new DistributerRequests
            {
                RegionId = regionId,
                ResourceId = model.ResourceId,
                TotalNeeded = model.TotalNeeded
            };
            distributerRequestRepo.Insert(newRequest);
            return RedirectToActionPermanent("Index");

        }

        public async Task<ActionResult> CheckResources()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            var availableResource = (from item in auditRepo.FilteredGet().Where(x => x.RegionId == regionId)
                                     join resources in resourceRepo.FilteredGet() on item.ResourceId equals resources.Id
                                     select new AvailableResourceViewModel
                                     {
                                          Id = item.Id,
                                           Name = resources.Name,
                                            Total = item.TotalCountAvailable
                                     }).ToList();
            return View(availableResource?? new System.Collections.Generic.List<AvailableResourceViewModel>());
        } 
    }
}