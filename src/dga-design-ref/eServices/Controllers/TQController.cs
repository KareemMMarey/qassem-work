using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace eServices.Controllers
{
    public class TQController : Controller
    {
        // TQ (Transaction Query) الاستعلام عن معاملة
        public IActionResult Index()
        {
            return View();
        }
    }
}
