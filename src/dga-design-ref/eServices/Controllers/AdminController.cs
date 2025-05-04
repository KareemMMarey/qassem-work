using Microsoft.AspNetCore.Mvc;

namespace eServices.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult AllRequests()
        {
            return View();
        }public IActionResult EditRequest()
        {
            return View();
        }
    }
}
