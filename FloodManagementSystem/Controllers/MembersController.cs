using FloodManagementSystem.Data.Models;
using FloodManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace FloodManagementSystem.Controllers
{
    [Authorize(Roles = "Members")]
    public class MembersController : Controller
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
        private readonly IRepository<DistributerRequests> dsitributerRequestrepo;

        public MembersController(IRepository<Data.Models.Resources> resourceRepo, IRepository<Data.Models.ResourceCollection> resourcCollectionRepo
            , IRepository<Data.Models.City> cityRepo, IRepository<Data.Models.State> stateRepo,
             IRepository<Data.Models.Regions> regionRepo, IRepository<Data.Models.EffectedCities> ecRepos,
              IRepository<Data.Models.AspNetUsers> userRepo,
               UserManager<Models.ApplicationUser> _userManager,
               IRepository<Data.Models.ResourceRequest> resourceRequestRepo,
               IRepository<DisasterDetails> ddRepo, IRepository<Disaster> dRep,
               IRepository<EffectedCities> ecRepo, IHostingEnvironment _environment,
                IRepository<DistributerRequests> dsitributerRequestrepo
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
            this.dsitributerRequestrepo = dsitributerRequestrepo;
        }

        // GET: Members
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            int stateId = (int)regionRepo.FilteredGet().Where(x => x.Id == regionId).Include("City").FirstOrDefault().City.StateId;
            var activeDisaster = ddRepo.FilteredGet().Where(x => x.IsActive == true).Include("Disaster").ToList();
            DisasterArea area = new DisasterArea { IsEffected = false };
            MemberHomeViewModel model = new MemberHomeViewModel
            {
                Disasters = (from item in activeDisaster
                             select new DisasterDetailsviewModel
                             {
                                 Id = item.Id,
                                 Name = item.Name,
                                 Details = item.Description,
                                 ImgURL = Path.Combine(_environment.WebRootPath, item.Disaster.ImagePath)
                             }).ToList() ?? new List<DisasterDetailsviewModel>(),
                Requests = (from item in ecRepo.FilteredGet().Where(x => x.Stateid == stateId && x.IsActive == true)
                            select new DisasterArea
                            {
                                DisasterDetailsId = item.Id,
                                IsEffected = true
                            }
                            ).FirstOrDefault() ?? new DisasterArea { IsEffected = false }
            };
            return View(model);
        }

        public async System.Threading.Tasks.Task<ActionResult> DonateResource(int disasterDetailsId, bool isSuccess = false)
        {
            return await ShowDonatePage(disasterDetailsId, isSuccess);
        }

        #region Functions
        private async System.Threading.Tasks.Task<ActionResult> ShowDonatePage(int disasterDetailsId, bool isSuccess)
        {
            List<EffectedPlaces> effectedStates =
                (from item in ecRepos.FilteredGet().Where(x => x.DisasterDetailsId == disasterDetailsId).Include(x => x.State).ThenInclude(x => x.City)
                 select new EffectedPlaces
                 {
                     Places = new PlaceModel
                     {
                         State = new AddressViewModel { Id = item.State.Id, Name = item.State.Name },
                         Cities = (from item2 in item.State.City
                                   select new CityViewModel
                                   {
                                       Id = item2.Id,
                                       City = item2.Name,
                                       Regions = (from item3 in regionRepo.FilteredGet().Where(x => x.CityId == item2.Id)
                                                  select new AddressViewModel
                                                  {
                                                      Id = item3.Id,
                                                      Name = item3.Name
                                                  }).ToList()
                                   }).ToList()
                     }
                 }).ToList();

            List<AddressViewModel> Regions = new List<AddressViewModel>();
            foreach (var item in effectedStates)
            {
                foreach (var item2 in item.Places.Cities)
                {
                    Regions.AddRange(item2.Regions);
                }
            }
            ViewBag.DonateSuccess = isSuccess;
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            ResourceModel model = new ResourceModel
            {
                Resource = (from item in resourceRepo.FilteredGet()
                            select new ViewModels.Resources
                            {
                                Id = item.Id,
                                Name = item.Name
                            }).ToList(),
                EffectedRegion = Regions,
                DisasterId = disasterDetailsId,
                RegionId = regionId
            };
            return View(model);
        }
        #endregion

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> DonateResource(ResourceModel model)
        {
            if (ModelState.IsValid)
            {
                System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
                var currentUser = await _userManager.GetUserAsync(currentUserClaims);
                var data = new ResourceCollection { ResourceId = model.ResourceId, RgionId = model.RegionId, TotalCollected = model.Count, Status = (int)Data.ResourceStatus.Requested, UserId = currentUser.Id };
                resourcCollectionRepo.Insert(data);
                return RedirectToAction("DonateResource", new { disasterDetailsId = model.DisasterId, isSuccess = true });
            }
            return await ShowDonatePage(model.DisasterId, false);
        }

        public async System.Threading.Tasks.Task<ActionResult> RequestResource(int disasterDetailsId, bool requestSuccess = false)
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            ViewBag.RequestSuccess = requestSuccess;
            ResourceModel model = new ResourceModel
            {
                Resource = (from item in resourceRepo.FilteredGet()
                            select new ViewModels.Resources
                            {
                                Id = item.Id,
                                Name = item.Name
                            }).ToList(),
                DisasterId = disasterDetailsId,
                RegionId = regionId

            };

            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> RequestResource(ResourceModel model)
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            ViewBag.RequestSuccess = false;
            if (ModelState.IsValid)
            {
                var requestRepo = new ResourceRequest
                {
                    DisasterDetailsId = model.DisasterId,
                    RegionId = model.RegionId,
                    RequestDetails = model.RequestDetails,
                    ResourceId = model.ResourceId,
                    TotalNeeded = model.Count,
                    ResourceStatus = (int)Data.ResourceStatus.Requested,
                    UserId = currentUser.Id
                };
                resourceRequestRepo.Insert(requestRepo);

                return RedirectToAction("RequestResource", new { disasterDetailsId = model.DisasterId, requestSuccess = true });
            }
            model.Resource = (from item in resourceRepo.FilteredGet()
                              select new ViewModels.Resources
                              {
                                  Id = item.Id,
                                  Name = item.Name
                              }).ToList();
            return View(model);
        }

        public async System.Threading.Tasks.Task<ActionResult> ShowDonataions()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            var data = (from item in resourcCollectionRepo.FilteredGet().Where(x => x.UserId == currentUser.Id)
                        select new MemberResourceListViewModel
                        {
                            Status = Convert.ToString((Data.ResourceStatus)item.Status),
                            ResourceType = resourceRepo.FilteredGet().Where(x => x.Id == item.ResourceId).FirstOrDefault().Name,
                            Count = item.TotalCollected
                        }
                        ).ToList();
            return View(data);
        }

        public async System.Threading.Tasks.Task<ActionResult> ShowRequest()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            var data = (from item in resourceRequestRepo.FilteredGet().Where(x => x.UserId == currentUser.Id)
                        select new MemberResourceListViewModel
                        {
                            Status = Convert.ToString((Data.ResourceStatus)item.ResourceStatus),
                            ResourceType = resourceRepo.FilteredGet().Where(x => x.Id == item.ResourceId).FirstOrDefault().Name,
                            Count = item.TotalNeeded
                        }
                        ).ToList();
            return View(data);
        }
         
    }
}