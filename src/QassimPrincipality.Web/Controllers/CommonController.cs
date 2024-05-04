using Microsoft.AspNetCore.Mvc;

namespace QassimPrincipality.Web.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
