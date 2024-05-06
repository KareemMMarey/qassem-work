using Framework.Core.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Application.Services.Main.UploadRequest;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Web.ViewModels.Contact;
using QassimPrincipality.Web.ViewModels.Request;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using QassimPrincipality.Application.Services.Main.ShareData;
using Microsoft.AspNetCore.Authorization;

namespace QassimPrincipality.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactFormAppService _contactService;
        private readonly LookupAppService _lookUpService;

        public ContactController(
            ContactFormAppService contactService, LookupAppService lookUpService
        )
        {
            _contactService = contactService;
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
        public async Task<ActionResult> Create(AddContactVM model)
        {
            if (!ModelState.IsValid) {
                ViewData["contacttypes"] = await _lookUpService.GetConatctType();
                return View(model);
            }
            ContactFormDto dto = new ContactFormDto();  
            dto.ContactTitle = model.ContactTitle;
            dto.UserEmail = model.UserEmail;
            dto.UserMobile = model.UserMobile;  
            dto.Description= model.Description;
            dto.IdentityNumber = model.IdentityNumber;
            dto.ContactTypeId=model.ContactTypeId;
            await _contactService.InsertAsync(dto);
            return RedirectToAction("Index", "Common");
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
            var result = await _contactService.SearchAsync(
                new ContactDataSearchDto()
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
            var result = await _contactService.GetById(Guid.Parse(requestId));

            return View(result);
        }
    }
}
