using Microsoft.AspNetCore.Mvc;

namespace eServices.Controllers
{
    public class ESController : Controller
    {
        //  ES (Electronic Summons) Forms الاستدعاء الالكنروني
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
