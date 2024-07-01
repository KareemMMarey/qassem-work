using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Web.Helpers;

namespace QassimPrincipality.Web.Controllers
{
    public class InqueryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> InqueryRequest(string RecordNo, string NationalNo)
        {
            var token = await ApiConsumer.ServicePostConsumerAsync<dynamic>(
            _nafathConfiguartion.Value.ApiUrl,
                _nafathConfiguartion.Value.NafathBody,
                headers.ToArray()
            );
            return View();
        }
    }
}
