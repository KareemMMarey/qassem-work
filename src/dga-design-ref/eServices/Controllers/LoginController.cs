using Microsoft.AspNetCore.Mvc;

namespace eServices.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OTP()
        {
            return View();
        }

        public IActionResult AccountSetup()
        {
            return View();
        }
    }
}
