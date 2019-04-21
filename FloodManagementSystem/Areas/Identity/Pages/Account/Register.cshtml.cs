using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FloodManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using FloodManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FloodManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FloodManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IRepository<Data.Models.State> stateRepo;
        private readonly IRepository<Data.Models.City> cityRepo;
        private readonly IRepository<Data.Models.Regions> regionsRepo;
        private readonly IRepository<Data.Models.AspNetUsers> userRepo;
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, IRepository<Data.Models.State> stateRepo, IRepository<Data.Models.City> cityRepo, IRepository<Data.Models.Regions> regionsRepo, IRepository<Data.Models.AspNetUsers> userRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.stateRepo = stateRepo;
            this.cityRepo = cityRepo;
            this.regionsRepo = regionsRepo;
            this.userRepo = userRepo;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<ViewModels.AddressViewModel> StatesList { get; set; }

        public List<ViewModels.CityViewModel> CityList { get; set; }

        public List<ViewModels.RegionViewModel> RegionList { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Phone Number")]
            [StringLength(10, ErrorMessage = "The {0} must be {2} characters long.", MinimumLength = 10)]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            [Range(1, int.MaxValue, ErrorMessage = "State field is required.")]
            [Display(Name = "State")]
            public int State { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "City field is required.")]
            [Display(Name = "City")]
            public int City { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "City field is required.")]
            [Display(Name = "Region")]
            public int Region { get; set; }

        }

        public void OnGet(string returnUrl = null)
        {
            StatesList = (from item in stateRepo.FilteredGet()
                          select new AddressViewModel
                          {
                              Id = item.Id,
                              Name = item.Name
                          }
                          ).
                          ToList();

            CityList = (from item in cityRepo.FilteredGet()
                        select new CityViewModel
                        {
                            Id = item.Id,
                            City = item.Name,
                            StateId = (int)item.StateId
                        }).
                          ToList();

            RegionList = (from item in regionsRepo.FilteredGet()
                          select new RegionViewModel
                          {
                              Id = item.Id,
                              Region = item.Name,
                              CityId = (int)item.CityId
                          }).
                          ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, "Members");
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var newUser = userRepo.FilteredGet().Where(x => x.Id == user.Id).SingleOrDefault();
                    newUser.RegionId = Input.Region;
                    userRepo.Update(newUser); 
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            StatesList = (from item in stateRepo.FilteredGet()
                          select new AddressViewModel
                          {
                              Id = item.Id,
                              Name = item.Name
                          }
                     ).
                     ToList();

            CityList = (from item in cityRepo.FilteredGet()
                        select new CityViewModel
                        {
                            Id = item.Id,
                            City = item.Name,
                            StateId = (int)item.StateId
                        }).
                          ToList();

            RegionList = (from item in regionsRepo.FilteredGet()
                          select new RegionViewModel
                          {
                              Id = item.Id,
                              Region = item.Name,
                              CityId = (int)item.CityId
                          }).
                          ToList();
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
