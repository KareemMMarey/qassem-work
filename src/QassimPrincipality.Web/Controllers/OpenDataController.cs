using Framework.Core.Extensions;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using QassimPrincipality.Application.Services.Main.OpenData;
using QassimPrincipality.Application.Services.NewShema;
using QassimPrincipality.Domain.Entities.Services.Main;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.ViewModels.OpenData;

namespace QassimPrincipality.Web.Controllers
{
    [Authorize]
    public class OpenDataController : Controller
    {
        private readonly OpenDataAppService _openService;
        private readonly LookupAppService _lookUpService;
        private readonly UserAppService _userAppService;
        private readonly ReferralNumberConfiguration _referralNumberConfiguration;

        public OpenDataController(
            OpenDataAppService openService,
            LookupAppService lookUpService,
            UserAppService userAppService,
            IOptions<ReferralNumberConfiguration> referralNumberConfiguration

        )
        {
            _openService = openService;
            _lookUpService = lookUpService;
            _userAppService = userAppService;
            _referralNumberConfiguration = referralNumberConfiguration.Value;

        }

        public async Task<IActionResult> Index(string type, int page = 1)
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
                new { Id = "0", Name = "طلبات منتهية بالرفض" },
                new { Id = "1", Name = "طلبات منتهية بالموافقة" },
                new { Id = "2", Name = "طلبات قيد الإجراء" },
                new { Id = "20", Name = "كل الطلبات" },
            };
            ViewBag.items = new SelectList(lst, "Id", "Name", type);

            ViewBag.status = type;
            var result = await _openService.SearchAsync(
                new OpenDataRequestSearchDto()
                {
                    isPending = isPending,
                    IsApproved = status,
                    PageNumber = page,
                    PageSize = 10,
                    CreatedBy = HttpContext.User.GetId()
                }
            );
            return View(result);
        }

        public async Task<ActionResult> Create()
        {
            AddOpenDataViewModel vM = new AddOpenDataViewModel();
            var user = await _userAppService.GetUserAsync(Guid.Parse(HttpContext.User.GetId()));
            vM.UserFullName = user.FullNameAr ?? user.FullName;
            vM.IdentityNumber = user.UserName.Replace("@nafath", "");
            //vM.IdentityNumber = vM.IdentityNumber.Replace("@nafath", "");
            var types = await _lookUpService.GetRequesterTypes();
            //vM.RequesterTypeId = int.Parse(types.First().Value);
            ViewData["requestertypes"] = types;
            return View(vM);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddOpenDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var types = await _lookUpService.GetRequesterTypes();
                //model.RequesterTypeId = int.Parse(types.First().Value);
                ViewData["requestertypes"] = types;
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
            dto.IsApproved = null;
            dto.CreatedBy = HttpContext.User.GetId();


            dto.ReferralNumber = _referralNumberConfiguration.OpenDataStart
                        + DateTime.Now.ToString("yyMMddHHmmss");


            var req = await _openService.InsertAsync(dto);

            return RedirectToAction(
                "Index",
                "Common",
                new
                {
                    SuccessMessage = "تم حفظ بيانات الطلب بنجاح",
                    requestNumber = req.ReferralNumber
                }
            );
        }

        [Authorize(Roles = "OpenDataRequestAdmin,Admin")]
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
                new { Id = "0", Name = "طلبات منتهية بالرفض" },
                new { Id = "1", Name = "طلبات منتهية بالموافقة" },
                new { Id = "2", Name = "طلبات قيد الإجراء" },
                new { Id = "20", Name = "كل الطلبات" },
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

        [Authorize(Roles = "OpenDataRequestAdmin,Admin")]
        public async Task<IActionResult> Accept(string requestId)
        {
            await _openService.AcceptOrReject(Guid.Parse(requestId), true);
            return RedirectToAction("Details", new { requestId });
        }

        [HttpPost]
        [Authorize(Roles = "OpenDataRequestAdmin,Admin")]
        public async Task<IActionResult> Reject(string requestId, string rejectReasons)
        {
            await _openService.AcceptOrReject(Guid.Parse(requestId), false, rejectReasons);
            return RedirectToAction("Details", new { requestId });
        }
    }
}
