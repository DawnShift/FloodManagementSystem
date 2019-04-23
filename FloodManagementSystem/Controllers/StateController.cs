using FloodManagementSystem.Data;
using FloodManagementSystem.Data.Models;
using FloodManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.Controllers
{
    [Authorize(Roles = "State Co-Ordinator")]
    public class StateController : Controller
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
        private readonly IRepository<StateAudit> stateAuditRepo;

        public StateController(IRepository<Data.Models.Resources> resourceRepo,
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
                IRepository<CityAudit> cityAuditRepo, IRepository<CityRequests> CityRequestsRepo,
             IRepository<StateAudit> stateAuditRepo
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
            this.cityAuditRepo = cityAuditRepo;
            this.CityRequestsRepo = CityRequestsRepo;
            this.stateAuditRepo = stateAuditRepo;
        }
        // GET: State
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetRequests()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            var regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            int cityId = (int)regionRepo.FilteredGet().Where(x => x.Id == regionId).FirstOrDefault().CityId;
            List<SqlParameter> paramList = new List<SqlParameter>
             {
                        new SqlParameter{  ParameterName ="cityId",Value=cityId,Direction =System.Data.ParameterDirection.Input},
             };
            var data = CityRequestsRepo.ReadFromStoredProcedure(StoredProc.GetCityRequests, paramList.ToArray()).ToList();
            var model = (from item in data
                         join city in cityRepo.FilteredGet() on item.CityId equals city.Id
                         join resource in resourceRepo.FilteredGet() on item.ResourceId equals resource.Id
                         select new StateListViewModel
                         {
                             Id = item.Id,
                             City = city.Name,
                             Resource = resource.Name,
                             Total = item.TotalNeeded
                         }
                         ).ToList();
            return    View(model ?? new List<StateListViewModel>());
        }

        public ActionResult Edit(int id)
        {
            var model = (from item in CityRequestsRepo.FilteredGet().Where(x => x.Id == id)
                         join city in cityRepo.FilteredGet() on item.CityId equals city.Id
                         join resource in resourceRepo.FilteredGet() on item.ResourceId equals resource.Id
                         select new StateListViewModel
                         {
                             Id = item.Id,
                             City = city.Name,
                             CityId = city.Id,
                             ResourceId = resource.Id,
                             Resource = resource.Name,
                             StateId = (int)city.StateId,
                             Total = item.TotalNeeded
                         }).FirstOrDefault();
            ViewBag.IsAvailable = true;
             var availableStock = stateAuditRepo.FilteredGet().Where(x => x.StateId == model.StateId && x.ResourceId == model.ResourceId).FirstOrDefault() ?? new StateAudit();
            if (availableStock.TotalAvailable < model.Total)
            {
                ViewBag.IsAvailable = false;
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(StateListViewModel model)
        {
            var availableResources = stateAuditRepo.FilteredGet().Where(x => x.ResourceId == model.ResourceId && x.StateId == model.StateId).FirstOrDefault() ?? new StateAudit();
            availableResources.TotalAvailable = availableResources.TotalAvailable - model.Total;
            stateAuditRepo.Update(availableResources);
            var addResources = cityAuditRepo.FilteredGet().Where(x => x.CityId == model.CityId && x.ResourceId == model.ResourceId).FirstOrDefault();
            if (addResources == null)
            {
                var res = new CityAudit
                {
                    ResourceId = model.ResourceId,
                    CityId = model.CityId,
                    TotalAvailable = model.Total
                };
                cityAuditRepo.Insert(res);
            }
            else
            {
                addResources.TotalAvailable = addResources.TotalAvailable + model.Total;
                cityAuditRepo.Update(addResources);
            }
            var request = CityRequestsRepo.FilteredGet().Where(x => x.Id == model.Id).FirstOrDefault();
            CityRequestsRepo.Delete(request);
            return RedirectToActionPermanent("GetRequests");
        }

        public ActionResult AddInventory()
        {
            var model = new InventoryViewModel();
            model.Resources = (from item in resourceRepo.FilteredGetAll()
                               select new AddressViewModel
                               {
                                   Id = item.Id,
                                   Name = item.Name
                               }
                               ).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AddInventory(InventoryViewModel model)
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            var regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            int cityId = (int)regionRepo.FilteredGet().Where(x => x.Id == regionId).FirstOrDefault().CityId;
            var stateId = (int)cityRepo.FilteredGet().Where(x => x.Id == cityId).FirstOrDefault().StateId;
            var resource = stateAuditRepo.FilteredGet().Where(x => x.StateId == stateId && x.ResourceId == model.ResourceId).FirstOrDefault();
            if (resource == null)
            {
                stateAuditRepo.Insert(new StateAudit { ResourceId = model.ResourceId, StateId = stateId, TotalAvailable = model.Total });
            }
            else
            {
                resource.TotalAvailable = resource.TotalAvailable + model.Total;
                stateAuditRepo.Update(resource);
            }
            return RedirectToActionPermanent("Index");
        }

            public async Task<ActionResult> CheckResources()
        {
            System.Security.Claims.ClaimsPrincipal currentUserClaims = this.User;
            var currentUser = await _userManager.GetUserAsync(currentUserClaims);
            int regionId = (int)userRepo.FilteredGet().Where(x => x.Id == currentUser.Id).FirstOrDefault().RegionId;
            int stateId = (int)regionRepo.FilteredGet().Where(x => x.Id == regionId).Include(y=>y.City).FirstOrDefault().City.StateId;
           var availableResource = (from item in stateAuditRepo.FilteredGet().Where(x => x.StateId == stateId)
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