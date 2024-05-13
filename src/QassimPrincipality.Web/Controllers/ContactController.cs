using Framework.Core.Extensions;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Web.ViewModels.Contact;

namespace QassimPrincipality.Web.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ContactFormAppService _contactService;
        private readonly LookupAppService _lookUpService;
        private readonly UserAppService _userAppService;

        public ContactController(
            ContactFormAppService contactService,
            LookupAppService lookUpService,
            UserAppService userAppService
        )
        {
            _contactService = contactService;
            _lookUpService = lookUpService;
            _userAppService = userAppService;
        }

        public async Task<IActionResult> Index(string type, int page = 1)
        {
            var result = await _contactService.SearchAsync(
                new ContactDataSearchDto()
                {
                    PageNumber = page,
                    PageSize = 10,
                    CreatedBy = HttpContext.User.GetId()
                }
            );
            return View(result);
        }

        public async Task<ActionResult> Create()
        {
            AddContactVM vM = new AddContactVM();
            var user = await _userAppService.GetUserAsync(Guid.Parse(HttpContext.User.GetId()));
            vM.UserFullName = user.FullNameAr ?? user.FullName;
            vM.IdentityNumber = "1234567899";
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
            if (!ModelState.IsValid)
            {
                ViewData["contacttypes"] = await _lookUpService.GetConatctType();
                return View(model);
            }
            ContactFormDto dto = new ContactFormDto();
            dto.UserFullName = model.UserFullName;
            dto.ContactTitle = model.ContactTitle;
            dto.UserEmail = model.UserEmail;
            dto.UserMobile = model.UserMobile;
            dto.Description = model.Description;
            dto.IdentityNumber = model.IdentityNumber;
            dto.ContactTypeId = model.ContactTypeId;
            dto.IsApproved = null;
            dto.CreatedBy = HttpContext.User.GetId();
            await _contactService.InsertAsync(dto);
            return RedirectToAction("Index", "Common");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RequestList(string type, int page = 1)
        {
            var result = await _contactService.SearchAsync(
                new ContactDataSearchDto() { PageNumber = page, PageSize = 10 }
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
