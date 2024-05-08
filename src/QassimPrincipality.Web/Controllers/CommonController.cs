using Microsoft.AspNetCore.Mvc;

namespace QassimPrincipality.Web.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Index(string SuccessMessage="", string requestNumber="")
        {
            ViewBag.SuccessMessage = SuccessMessage;
            ViewBag.requestNumber = requestNumber;
            return View();
        }
    }
}
