using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Application.Services.Main.ShareData;
using QassimPrincipality.Application.Services.Main.ShareDataRequest;
using QassimPrincipality.Web.ViewModels.Contact;
using QassimPrincipality.Web.ViewModels.ShareData;

namespace QassimPrincipality.Web.Controllers
{
    public class ShareDataController : Controller
    {
        private readonly ShareDataAppService _shareDataService;
        private readonly LookupAppService _lookUpService;

        public ShareDataController(
            ShareDataAppService shareDataService, LookupAppService lookUpService
        )
        {
            _shareDataService = shareDataService;
            _lookUpService = lookUpService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create()
        {
            AddContactVM vM = new AddContactVM();
            vM.UserFullName = "Kareem marey";
            vM.IdentityNumber = "123232123231";
            ViewData["contacttypes"] = await _lookUpService.GetConatctType();
            return View(vM);
        }
        public ActionResult MessageResult()
        {
            return View();
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddShareDataVM model)
        {
            if (!ModelState.IsValid)
            {
               
                return View();
            }
            ShareDataDto dto = new ShareDataDto();
            dto.ContactTitle = model.ContactTitle;
            dto.UserEmail = model.UserEmail;
            dto.UserMobile = model.UserMobile;
            dto.Description = model.Description;
            dto.IdentityNumber = model.IdentityNumber;
            await _shareDataService.InsertAsync(dto);
            return RedirectToAction("Common", "Index");
        }
    }
}
