using eServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eServices.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult About()
        {
            return View();
        }
        public IActionResult News(int Id)
        {
            ViewData["NewsId"] = Id;
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    
        public IActionResult TermsAndConditions()
        {
            return View();
        }
        
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult ServiceLevelAgreement()
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
