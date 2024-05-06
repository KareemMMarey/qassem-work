using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Application.Services.Main.OpenData;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
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
            var result = await _openService.SearchAsync(
                new OpenDataRequestSearchDto()
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
            var result = await _openService.GetById(Guid.Parse(requestId));

            return View(result);
        }
    }
}
