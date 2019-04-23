using FloodManagementSystem.Data;
using FloodManagementSystem.Data.Models;
using FloodManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.Controllers
{
    [Authorize(Roles = "District Co-Ordinator")]
    public class DistrictController : Controller
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
        private readonly IRepository<CityAudit> cityAuditRepo;
        private readonly IRepository<CityRequests> CityRequestsRepo;
        private readonly IRepository<DistributerRequests> distributerRequestRepo;
        public DistrictController(IRepository<Data.Models.Resources> resourceRepo,
            IRepository<Data.Models.ResourceCollection> resourcCollectionRepo
            , IRepository<Data.Models.City> cityRepo, IRepository<Data.Models.State> stateRepo,
             IRepository<Data.Models.Regions> regionRepo, IRepository<Data.Models.EffectedCities> ecRepos,
              IRepository<Data.Models.AspNetUsers> userRepo,
               UserManager<Models.ApplicationUser> _userManager,
               IRepository<Data.Models.ResourceRequest> resourceRequestRepo,
               IRepository<DisasterDetails> ddRepo, IRepository<Disaster> dRep,
                IRepository<ResourceAudit> auditRepo,
               IRepository<EffectedCities> ecRepo, IHostingEnvironment _environment,
                IRepository<DistributerRequests> distributerRequestRepo,
                IRepository<CityAudit> cityAuditRepo, IRepository<CityRequests> CityRequestsRepo)
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
            this.cityAuditRepo = cityAuditRepo;
            this.CityRequestsRepo = CityRequestsRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> GetRequests()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            var region = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            List<SqlParameter> paramList = new List<SqlParameter>
             {
                        new SqlParameter{  ParameterName ="regionId",Value=region,Direction =System.Data.ParameterDirection.Input},
             };
            var data = distributerRequestRepo.ReadFromStoredProcedure(StoredProc.GetDistributerrequests, paramList.ToArray());
            var model = (from item in data
                         join resources in resourceRepo.FilteredGet() on item.ResourceId equals resources.Id
                         join regions in regionRepo.FilteredGet() on item.RegionId equals regions.Id
                         select new DistrictListViewModel
                         {
                             Id = item.Id,
                             Region = regions.Name,
                             Resource = resources.Name,
                             TotalNeeded = item.TotalNeeded,
                             ResourceId = item.ResourceId,
                             RegionId = item.RegionId
                         }).ToList();

            return View(model??new List<DistrictListViewModel>());
        }

        public ActionResult Edit(int id)
        {
            var model = (from item in distributerRequestRepo.FilteredGet().Where(x => x.Id == id)
                         join resources in resourceRepo.FilteredGet() on item.ResourceId equals resources.Id
                         join regions in regionRepo.FilteredGet() on item.RegionId equals regions.Id
                         select new DistrictListViewModel
                         {
                             Id = item.Id,
                             Region = regions.Name,
                             Resource = resources.Name,
                             TotalNeeded = item.TotalNeeded,
                             ResourceId = item.ResourceId,
                             RegionId = item.RegionId
                         }).FirstOrDefault();
            ViewBag.IsAvailable = true;
            int cityId = (int)regionRepo.FilteredGet().Where(x => x.Id == model.RegionId).FirstOrDefault().CityId;
            var availableResource = cityAuditRepo.FilteredGet().Where(x => x.ResourceId == model.ResourceId && x.CityId == cityId).FirstOrDefault() ?? new CityAudit();
            if (availableResource.TotalAvailable < model.TotalNeeded)
            {
                ViewBag.IsAvailable = false;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(DistrictListViewModel model)
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            var regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            int cityId = (int)regionRepo.FilteredGet().Where(x => x.Id == regionId).FirstOrDefault().CityId;
            var availableResources = cityAuditRepo.FilteredGet().Where(x => x.CityId == cityId && x.ResourceId == model.ResourceId).FirstOrDefault() ?? new CityAudit(); ;
            availableResources.TotalAvailable = availableResources.TotalAvailable - model.TotalNeeded;
            cityAuditRepo.Update(availableResources);
            var addedResource = auditRepo.FilteredGet().Where(x => x.RegionId == regionId && x.ResourceId == model.ResourceId).FirstOrDefault();
            if (addedResource != null)
            {
                addedResource.TotalCountAvailable = addedResource.TotalCountAvailable + model.TotalNeeded;
                auditRepo.Update(addedResource);
            }
            else
            {
                var res = new ResourceAudit
                {
                    RegionId = regionId,
                    CityId = cityId,
                    ResourceId = model.ResourceId,
                    StateId = 1,
                    TotalCountAvailable = model.TotalNeeded,
                };
                auditRepo.Insert(res);
            }
            var request = distributerRequestRepo.FilteredGet().Where(x => x.Id == model.Id).FirstOrDefault();
            distributerRequestRepo.Delete(request);
            return RedirectToActionPermanent("GetRequests");
        }

        public ActionResult RequestResource()
        {
            var model = new DistrictRequestListViewModel();
            model.Resources = (from item in resourceRepo.FilteredGet()
                               select new AddressViewModel
                               {
                                   Id = item.Id,
                                   Name = item.Name
                               }).ToList();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> RequestResource(DistrictRequestListViewModel model)
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            var regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            int cityId = (int)regionRepo.FilteredGet().Where(x => x.Id == regionId).FirstOrDefault().CityId;
            var data = new CityRequests
            {
                CityId = cityId,
                ResourceId = model.ResourceId,
                TotalNeeded = model.TotalNeeded
            };
            CityRequestsRepo.Insert(data);
            return RedirectToActionPermanent("Index");
        }


        public async Task<ActionResult> CheckResources()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            int cityId = (int)regionRepo.FilteredGet().Where(x => x.Id == regionId).FirstOrDefault().CityId;
            var availableResource = (from item in cityAuditRepo.FilteredGet().Where(x => x.CityId == cityId)
                                     join resources in resourceRepo.FilteredGet() on item.ResourceId equals resources.Id
                                     select new AvailableResourceViewModel
                                     {
                                         Id = item.Id,
                                         Name = resources.Name,
                                         Total = item.TotalAvailable
                                     }).ToList();
            return View(availableResource ?? new System.Collections.Generic.List<AvailableResourceViewModel>());
        }

    }
}