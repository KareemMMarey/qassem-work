using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace eServices.Controllers
{
    public class PRTFController : Controller
    {
        // PR-TF (Prisoner Request - Transfer Facility) Forms طلب نقل سجين من سجن لآخر
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
