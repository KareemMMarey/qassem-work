using Microsoft.AspNetCore.Mvc;

namespace eServices.Controllers
{
    public class PRTRController : Controller
    {
        // PR-TR (Prisoner Request - Temporary Release) Forms طلب الخروج المؤقت للسجين
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
