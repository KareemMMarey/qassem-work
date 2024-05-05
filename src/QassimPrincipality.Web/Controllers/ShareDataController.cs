using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Application.Services.Main.OpenData;
using QassimPrincipality.Application.Services.Main.ShareData;
using QassimPrincipality.Application.Services.Main.ShareDataRequest;
using QassimPrincipality.Web.ViewModels.Contact;
using QassimPrincipality.Web.ViewModels.OpenData;
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
            AddShareDataVM vM = new AddShareDataVM();
            vM.UserFullName = "Kareem marey";
            vM.IdentityNumber = "123232123231";
            vM.LegalJustificationDescription = "وصف المسوغ القانوني";
            ViewData["entities"] = await _lookUpService.GetEntities();
            return View(vM);
        }


        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddShareDataVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["entities"] = await _lookUpService.GetEntities();
                return View(model);
            }
            ShareDataDto dto = new ShareDataDto();
            dto.PurposeOfRequest = model.PurposeOfRequest;
            dto.Description = model.Description;
            dto.UserEmail = model.UserEmail;
            dto.UserMobile = model.UserMobile;
            dto.IdentityNumber = model.IdentityNumber;
            dto.UserFullName = model.UserFullName;
            dto.EntityTypeId = model.EntityId;
            dto.IsLegalJustification = model.IsLegalJustification== "true" ? true:false;
            dto.LegalJustificationDescription = model.LegalJustificationDescription;
            dto.IsRequesterDataOfficePresenter = model.IsRequesterDataOfficePresenter == "true" ? true : false; ;
            dto.IsContainsPersonalData = model.IsContainsPersonalData == "true" ? true : false; 
            dto.IsShareAgreementExist = model.IsShareAgreementExist == "true" ? true : false; 
            await _shareDataService.InsertAsync(dto);
            return RedirectToAction("Index", "Common", new { SuccessMessage="تم حفظ طلب مشاركة البيانات"});
        }
    }
}
