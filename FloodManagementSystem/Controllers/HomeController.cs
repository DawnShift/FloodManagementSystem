using FloodManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace FloodManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            if (HttpContext.User.Identities.First().Name == null)
                return View();
            else
            {
                if (HttpContext.User.IsInRole("Members"))
                    return RedirectPermanent("/Members/Index");
                if (HttpContext.User.IsInRole("Distributer"))
                    return RedirectPermanent("/Distributer/Index");
                if (HttpContext.User.IsInRole("District Co-Ordinator"))
                    return RedirectPermanent("/District/Index");
                if (HttpContext.User.IsInRole("State Co-Ordinator"))
                    return RedirectPermanent("/State/Index"); 
                if (HttpContext.User.IsInRole("Administrator"))
                    return RedirectPermanent("/Master/Index");
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
