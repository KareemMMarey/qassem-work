using Framework.Core.Extensions;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Application.Services.Main.UploadRequest;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.ViewModels.Request;

namespace QassimPrincipality.Web.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly UploadRequestAppService _uploadRequestService;
        private readonly RequestTypeAppService _requestTypeAppService;
        private readonly UserAppService _userServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ReferralNumberConfiguration _referralNumberConfiguration;

        public RequestController(
            UploadRequestAppService uploadRequestAppService,
            RequestTypeAppService requestTypeAppService,
            UserAppService userServices,
            UserManager<ApplicationUser> userManager,
            IOptions<ReferralNumberConfiguration> referralNumberConfiguration
        )
        {
            _uploadRequestService = uploadRequestAppService;
            _requestTypeAppService = requestTypeAppService;
            _userServices = userServices;
            _userManager = userManager;
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
            var result = await _uploadRequestService.SearchAsync(
                new UploadRequestSearchDto()
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


        public async Task<IActionResult> Details(string requestId)
        {
            var result = await _uploadRequestService.GetById(Guid.Parse(requestId));

            return View(result);
        }

        // GET: RequestController/Create
        public async Task<ActionResult> Create()
        {
            ViewData["RequestTypes"] = await _requestTypeAppService.GetAllRequestTypes();
            return View();
        }

        // POST: RequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddRequestViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //var currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                var dto = new UploadRequestDtoAdd
                {
                    RequestDate = DateTime.Now,
                    RequestTypeId = model.RequestTypeId,
                    PhoneNumber = model.PhoneNumber,
                    NafathNumber = model.NafathNumber,
                    RequestName = model.RequestName,
                    Photo = model.Photo,
                    OtherAttachments = model.ListAttachments,
                    CreatedBy = HttpContext.User.GetId(),
                    ReferralNumber =
                        _referralNumberConfiguration.UploadRequestStart
                        + DateTime.Now.ToString("yyMMddHHmmss")
                };
                var res = await _uploadRequestService.InsertAsync(dto);
                return RedirectToAction(
                    "Index",
                    "Common",
                    new { SuccessMessage = "تم حفظ بيانات الطلب بنجاح", requestNumber = res.ReferralNumber }
                );
            }
            catch
            {
                ViewData["RequestTypes"] = await _requestTypeAppService.GetAllRequestTypes();

                return View();
            }
        }

        // GET: RequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
