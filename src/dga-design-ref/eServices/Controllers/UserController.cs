using Microsoft.AspNetCore.Mvc;

namespace eServices.Controllers
{
    public class UserController : Controller
    {
        public IActionResult MyProfile()
        {
            return View();
        }

        public IActionResult MyRequests()
        {
            return View();
        }

        public IActionResult RequestDetails()
        {
            return View();
        }
    }
}
