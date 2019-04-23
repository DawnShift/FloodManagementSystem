using FloodManagementSystem.Data;
using FloodManagementSystem.Data.Models;
using FloodManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MasterController : Controller
    {
        private readonly IRepository<City> cityRepo;
        private readonly IRepository<Data.Models.State> stateRepo;
        private readonly IRepository<Regions> regionRepo;
        private readonly IRepository<Data.Models.Resources> resourceRepo;
        private readonly IRepository<Data.Models.Professionals> professionalRepo;
        private readonly IRepository<Data.Models.Disaster> disasterRepo;
        private readonly IRepository<Data.Models.DisasterDetails> ddRepo;
        private readonly IHostingEnvironment _environment;
        private readonly IRepository<Data.Models.EffectedCities> ecRepos;
        private readonly UserManager<Models.ApplicationUser> _userManager;

        public MasterController(IRepository<City> cityRepo, IRepository<Data.Models.State> stateRepo, IRepository<Regions> regionRepo,
        IRepository<Data.Models.Resources> resourceRepo, IRepository<Data.Models.Professionals> professionalRepo, IRepository<Data.Models.Disaster> disasterRepo,
        IRepository<Data.Models.DisasterDetails> ddRepo, IRepository<Data.Models.EffectedCities> ecRepos, UserManager<Models.ApplicationUser> _userManager,
        IHostingEnvironment _environment)
        {
            this.cityRepo = cityRepo;
            this.stateRepo = stateRepo;
            this.regionRepo = regionRepo;
            this.professionalRepo = professionalRepo;
            this.resourceRepo = resourceRepo;
            this._environment = _environment;
            this.disasterRepo = disasterRepo;
            this.ddRepo = ddRepo;
            this.ecRepos = ecRepos;
            this._userManager = _userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllAvailableDisasters()
        {
            var activeDisaster = (from item in ddRepo.FilteredGet()
                                  join disasters in disasterRepo.FilteredGet() on item.DisasterId equals
                                  disasters.Id
                                  select new DisasterDetailsviewModel
                                  {
                                      Id = item.Id,
                                      Details = item.Description,
                                      Name = item.Name,
                                      EndDate = item.EndDate ?? DateTime.Now.AddDays(30),
                                      CreatedDate = item.CreatedDate,
                                      IsActive = item.IsActive,
                                      StartDate = item.StartDate,
                                  }).ToList();
            return View(activeDisaster ?? new List<DisasterDetailsviewModel>());
        }

        public ActionResult State()
        {
            ViewBag.StateIsSuccess = false;
            ViewBag.CityIsSuccess = false;
            ViewBag.RegionIsSuccess = false;
            return View(GetAllMasterDetails());
        }

        public ActionResult Disaster()

        {
            ViewBag.IsSuccess = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Disaster(ViewModels.DisasterViewModel model)
        {
            ViewBag.IsSuccess = false;
            string filePath = "";
            if (model.File != null)
            {
                string newName = $"_ {DateTime.Now.Millisecond}" + model.File.FileName;
                var path = Path.Combine(_environment.WebRootPath + "\\Resources\\",
                    newName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                filePath = @"\Resources\" + newName;
            }
            var dis = disasterRepo.FilteredGet().Where(x => x.Name.ToUpper() == model.Name.ToUpper()).FirstOrDefault();
            if (dis == null)
            {
                var newDisaster = new Data.Models.Disaster
                {
                    Name = model.Name,
                    ImagePath = filePath
                };
                disasterRepo.Insert(newDisaster);
                ViewBag.IsSuccess = true;
            }
            else
            {
                ModelState.AddModelError("Name", "Disaster Already Exists.");
            }
            return View();
        }

        public ActionResult Resource()
        {
            ViewBag.ItemSuccess = false;
            ViewBag.ResourceSucess = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resource(ViewModels.ResourceViewModel model)
        {
            bool isSuccess = false;
            var data = (resourceRepo.FilteredGet().Where(x => x.Name.ToUpper() == model.Resources.Name.ToUpper())).FirstOrDefault();
            if (data == null)
            {
                var newResource = new Data.Models.Resources
                {
                    Name = model.Resources.Name
                };
                resourceRepo.Insert(newResource);
                isSuccess = true;
            }
            else
            {
                ModelState.AddModelError("Resources.Name", "Resource With Name Already Exists.");
            }
            ViewBag.ItemSuccess = isSuccess;
            ViewBag.ResourceSucess = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult State(ViewModels.StateViewModel model)
        {
            bool isSuccess = false;
            var data = (stateRepo.FilteredGet().Where(x => x.Name.ToUpper() == model.States.Name.ToUpper())).FirstOrDefault();
            if (data == null)
            {
                var newState = new Data.Models.State { Name = model.States.Name };
                stateRepo.Insert(newState);
                isSuccess = true;
            }
            else
                ModelState.AddModelError("States.Name", "State With Name Already Exists.");
            ViewBag.StateIsSuccess = isSuccess;
            ViewBag.CityIsSuccess = false;
            ViewBag.RegionIsSuccess = false;
            return View(GetAllMasterDetails());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult City(ViewModels.StateViewModel model)
        {
            bool isSuccess = false;
            if (model.City.StateId < 1)
            {
                ViewBag.StateIsSuccess = false;
                ViewBag.CityIsSuccess = false;
                ViewBag.RegionIsSuccess = false;
                ModelState.AddModelError("City.StateId", "State Feild is Required.");
                return View("State", GetAllMasterDetails());
            }
            var data = (cityRepo.FilteredGet().Where(x => x.Name.ToUpper() == model.City.City.ToUpper())).FirstOrDefault();
            if (data == null)
            {
                var newCity = new Data.Models.City { Name = model.City.City, StateId = model.City.StateId };
                cityRepo.Insert(newCity);
                isSuccess = true;
            }
            else
                ModelState.AddModelError("City.Name", "City With Name Already Exists.");
            ViewBag.StateIsSuccess = false;
            ViewBag.CityIsSuccess = isSuccess;
            ViewBag.RegionIsSuccess = false;
            return View("State", GetAllMasterDetails());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Region(ViewModels.StateViewModel model)
        {
            bool isSuccess = false;
            if (model.Region.CityId < 1)
            {
                ViewBag.StateIsSuccess = false;
                ViewBag.CityIsSuccess = false;
                ViewBag.RegionIsSuccess = false;
                ModelState.AddModelError("Region.CityId", "City Feild is Required.");
                return View("State", GetAllMasterDetails());
            }
            var data = (regionRepo.FilteredGet().Where(x => x.Name.ToUpper() == model.Region.Region.ToUpper())).FirstOrDefault();
            if (data == null)
            {
                var newRegion = new Data.Models.Regions { Name = model.Region.Region, CityId = model.Region.CityId };
                regionRepo.Insert(newRegion);
                isSuccess = true;
            }
            else
                ModelState.AddModelError("Region.Name", "Region With Name Already Exists.");
            ViewBag.StateIsSuccess = false;
            ViewBag.CityIsSuccess = false;
            ViewBag.RegionIsSuccess = isSuccess;
            return View("State", GetAllMasterDetails());
        }

        public ActionResult AddDisaster()
        {
            DisasterDetailsviewModel model = new DisasterDetailsviewModel();
            model.States = GetAllMasterDetails().City.State;
            model.Disasters = (from item in disasterRepo.FilteredGet()
                               select new AddressViewModel
                               {
                                   Id = item.Id,
                                   Name = item.Name
                               }).ToList();
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult AddDisaster(DisasterDetailsviewModel model)
        {
            var disaster = new Data.Models.DisasterDetails
            {
                Name = model.Name,
                Description = model.Details,
                DisasterId = model.DisasterId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                StartDate = model.StartDate,
            };
            ddRepo.Insert(disaster);

            var effectedCiteis = (from item in model.EffectedStates
                                  select new EffectedCities
                                  {
                                      DisasterDetailsId = disaster.Id,
                                      Stateid = item
                                  }).ToList();

            ecRepos.InsertMany(effectedCiteis);
            RedirectToActionPermanent("AddDisaster");
            return View(model);
        }

        private StateViewModel GetAllMasterDetails()
        {
            List<AddressViewModel> stateList = (from item in stateRepo.FilteredGet()
                                                select new AddressViewModel
                                                {
                                                    Id = item.Id,
                                                    Name = item.Name
                                                }).ToList();
            List<AddressViewModel> cityList = (from item in cityRepo.FilteredGet()
                                               select new AddressViewModel
                                               {
                                                   Id = item.Id,
                                                   Name = item.Name
                                               }).ToList();
            return new StateViewModel { City = new CityViewModel { State = stateList }, Region = new RegionViewModel { City = cityList } };
        }

        //UserList
        public async Task<ActionResult> PramoteUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            string activerole = role[0];
            int roleId = UserRoles.Roles.Where(x => x.Role == activerole).First().Id;
            var userList = (from item in _userManager.Users.Where(x => x.Id != user.Id).ToList()
                            select new User
                            {
                                Id = item.Id,
                                Name = (item.FirstName ?? "") + " " + (item.LastName ?? ""),
                                Email = item.Email,
                            }).ToList();
            foreach (User users in userList)
            {
                var usrs = _userManager
                    .Users.Where(x => x.Id == users.Id).FirstOrDefault();
                users.Roles = await _userManager.GetRolesAsync(usrs);
                users.Role = users.Roles[0];
                users.RoleId = UserRoles.GetRoleId(users.Role);
            }
            return View(userList.Where(x => x.RoleId < roleId).ToList());
        }

        //channge Role
        public async Task<ActionResult> UpdateUserRoleAsync(string id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(currentUser);
            string activerole = role[0];
            int roleId = UserRoles.Roles.Where(x => x.Role == activerole).First().Id;
            var item = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            var currentRole = await _userManager.GetRolesAsync(item);
            var user = new User
            {
                Id = item.Id,
                Name = (item.FirstName ?? "") + " " + (item.LastName ?? ""),
                Email = item.Email,
                Username = item.UserName,
                Role = currentRole.FirstOrDefault()
            };

            ViewBag.Roles = (from val in UserRoles.Roles
                             where val.Id < roleId
                             select new SelectListItem
                             {
                                 Text = val.Role,
                                 Value = val.Role
                             }).ToList();
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUserRoleAsync(User model)
        {
            var user = _userManager.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            model.Roles = await _userManager.GetRolesAsync(user);
            var re = await _userManager.RemoveFromRoleAsync(user, model.Roles[0]);
            var res = await _userManager.AddToRoleAsync(user, model.Role);
            return RedirectToAction("PramoteUser");
        }
          
    }
}