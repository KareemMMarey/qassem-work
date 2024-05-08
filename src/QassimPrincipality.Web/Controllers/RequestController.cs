using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Application.Services.Main.UploadRequest;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Web.ViewModels.Request;

namespace QassimPrincipality.Web.Controllers
{
    public class RequestController : Controller
    {
        private readonly UploadRequestAppService _uploadRequestService;
        private readonly RequestTypeAppService _requestTypeAppService;
        private readonly UserAppService _userServices;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(
            UploadRequestAppService uploadRequestAppService,
            RequestTypeAppService requestTypeAppService,
             UserAppService userServices, UserManager<ApplicationUser> userManager
        )
        {
            _uploadRequestService = uploadRequestAppService;
            _requestTypeAppService = requestTypeAppService;
            _userServices = userServices;
            _userManager = userManager;
        }

        // GET: RequestController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RequestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                var currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                var dto = new UploadRequestDtoAdd
                {
                    RequestDate = DateTime.Now,
                    RequestTypeId = model.RequestTypeId,
                    PhoneNumber = model.PhoneNumber,
                    NafathNumber = model.NafathNumber,
                    RequestName = model.RequestName,
                    Photo = model.Photo,
                    OtherAttachments = model.ListAttachments,
                    CreatedByFullName="مستخدم اختبار"
                };
                var res = await _uploadRequestService.InsertAsync(dto);
                return RedirectToAction(
                    "Index",
                    "Common",
                    new { SuccessMessage = "تم حفظ بيانات الطلب بنجاح", requestNumber = res.referralNumber }
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
