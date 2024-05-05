using Microsoft.AspNetCore.Mvc;

namespace QassimPrincipality.Web.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Index(string SuccessMessage="")
        {
            ViewBag.SuccessMessage = SuccessMessage;
            return View();
        }
    }
}
