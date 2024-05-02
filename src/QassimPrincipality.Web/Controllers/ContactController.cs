using Framework.Core.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Lookups.Main.Contact;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Application.Services.Main.UploadRequest;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Web.ViewModels.Contact;
using QassimPrincipality.Web.ViewModels.Request;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

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
            await _contactService.InsertAsync(dto);
            return RedirectToAction("Common", "Index");
        }
    }
}
