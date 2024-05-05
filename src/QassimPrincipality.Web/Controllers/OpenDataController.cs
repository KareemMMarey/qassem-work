using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Application.Services.Main.OpenData;
using QassimPrincipality.Web.ViewModels.Contact;
using QassimPrincipality.Web.ViewModels.OpenData;
using QassimPrincipality.Web.ViewModels.ShareData;

namespace QassimPrincipality.Web.Controllers
{
    public class OpenDataController : Controller
    {
        private readonly OpenDataAppService _openService;
        private readonly LookupAppService _lookUpService;

       
        public OpenDataController(
            OpenDataAppService openService, LookupAppService lookUpService
        )
        {
            _openService = openService;
            _lookUpService = lookUpService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create()
        {
            AddOpenDataViewModel vM = new AddOpenDataViewModel();
            vM.UserFullName = "Kareem marey";
            vM.IdentityNumber = "123232123231";
            ViewData["requestertypes"] = await _lookUpService.GetRequestType();
            return View(vM);
        }
       

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddOpenDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["requestertypes"] = await _lookUpService.GetRequestType();
                return View(model);
            }
            OpenDataDto dto = new OpenDataDto();
            dto.Title = model.Title;
            dto.UserEmail = model.UserEmail;
            dto.UserFullName = model.UserFullName;  
            dto.UserMobile = model.UserMobile;
            dto.Description = model.Description;
            dto.IdentityNumber = model.IdentityNumber;
            dto.RequesterTypeId = model.RequesterTypeId;
            await _openService.InsertAsync(dto);
            return RedirectToAction("Common", "Index");
        }
    }
}
