using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Application.Services.Main.OpenData;
using QassimPrincipality.Web.ViewModels.Contact;
using QassimPrincipality.Web.ViewModels.OpenData;

namespace QassimPrincipality.Web.Controllers
{
    public class OpenDataController : Controller
    {
        private readonly OpenDataAppService _openService;

        public OpenDataController(
            OpenDataAppService openService, LookupAppService lookUpService
        )
        {
            _openService = openService;
            //_lookUpService = lookUpService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create()
        {
           
            return View();
        }
       

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddOpenDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //ViewData["contacttypes"] = await _lookUpService.GetConatctType();
                return View(model);
            }
            OpenDataDto dto = new OpenDataDto();
            dto.ContactTitle = model.ContactTitle;
            dto.UserEmail = model.UserEmail;
            dto.UserMobile = model.UserMobile;
            dto.Description = model.Description;
            dto.IdentityNumber = model.IdentityNumber;
            await _openService.InsertAsync(dto);
            return RedirectToAction("Common", "Index");
        }
    }
}
