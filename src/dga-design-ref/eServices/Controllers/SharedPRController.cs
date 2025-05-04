using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace eServices.Controllers
{
    public class SharedPRController : Controller
    {
        // ND-NS (Non-Deportation Request - Non-Saudi) طلب عدم الإبعاد من المملكة العربية السعودية (لغير السعودي)
        // TR-LR (Travel Request - List Removal) طلب السماح له بالسفر ورفع اسمه من القائمة (للسعودي)
        // RR-RI (Record Removal - Rehabilitation) طلب محو سابقة - رد اعتبار
        // Forms 
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
