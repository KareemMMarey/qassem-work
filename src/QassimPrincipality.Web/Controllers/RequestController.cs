using Framework.Core.AutoMapper;
using Microsoft.AspNetCore.Http;
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

        public RequestController(
            UploadRequestAppService uploadRequestAppService,
            RequestTypeAppService requestTypeAppService
        )
        {
            _uploadRequestService = uploadRequestAppService;
            _requestTypeAppService = requestTypeAppService;
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
                var dto = new UploadRequestDtoAdd
                {
                    RequestDate = DateTime.Now,
                    RequestTypeId = model.RequestTypeId,
                    PhoneNumber = model.PhoneNumber,
                    NafathNumber = model.NafathNumber,
                    RequestName = model.RequestName,
                    Photo = model.Photo,
                    OtherAttachments = model.ListAttachments
                };

                try
                {
                    await _uploadRequestService.InsertAsync(dto);

                }
                catch (Exception e)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
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
