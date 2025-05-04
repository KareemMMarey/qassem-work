using Microsoft.AspNetCore.Mvc;

namespace eServices.Controllers
{
    public class Services : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutTheService(string id)
        {
            ViewData["SelectedId"] = id;
            return View();
        }

        public IActionResult SuccessRequest()
        {
            return View();
        }
    }
}
