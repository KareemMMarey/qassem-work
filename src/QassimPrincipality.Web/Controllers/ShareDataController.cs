using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RequestList(string type, int page = 1)
        {
            bool? status = null;
            bool? isPending = null;
            switch (type)
            {
                case "1":
                    status = true;
                    break;
                case "0":
                    status = false;
                    break;
                case "2":
                    status = null;
                    isPending = true;
                    break;
                default:
                    status = null;
                    type = "20";
                    break;
            }

            var lst = new List<object>
            {
                new {Id = "0",Name="طلبات منتهية بالرفض"},
                new {Id = "1",Name="طلبات منتهية بالموافقة"},
                new {Id = "2",Name="طلبات قيد الإجراء"},
                new {Id = "20",Name="كل الطلبات"},
            };


            ViewBag.items = new SelectList(lst, "Id", "Name", type);
            ViewBag.status = type;
            var result = await _shareDataService.SearchAsync(
                new ShareDataRequestSearchDto()
                {
                    IsApproved = status,
                    PageNumber = page,
                    PageSize = 10
                }
            );
            return View(result);
        }
        public async Task<IActionResult> Details(string requestId)
        {
            var result = await _shareDataService.GetById(Guid.Parse(requestId));

            return View(result);
        }
    }
}
