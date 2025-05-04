using Microsoft.AspNetCore.Mvc;

namespace eServices.Controllers
{
    public class MSController : Controller
    {
        // MS (Marriage of Saudi) Forms خدمات الزواج
        public IActionResult StepOne()
        {
            return View();
        }

        public IActionResult StepTwo()
        {
            return View();
        }

        public IActionResult StepThree()
        {
            return View();
        }

        public IActionResult ReviewRequest()
        {
            return View();
        }
    }
}
