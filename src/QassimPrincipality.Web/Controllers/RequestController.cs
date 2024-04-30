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
                MemoryStream streamPhoto = new MemoryStream();
                model.Photo.CopyTo(streamPhoto);

                var dto = new UploadRequestDtoAdd
                {
                    RequestDate = DateTime.Now,
                    RequestTypeId = model.RequestTypeId,
                    PhoneNumber = model.PhoneNumber,
                    NafathNumber = model.NafathNumber,
                    RequestName = model.RequestName,
                    Photo = new Application.Dtos.AttachmentDto
                    {
                        FileContentData = streamPhoto.ToArray(),
                        FileName = model.Photo.FileName,
                    },
                    OtherAttachments = new List<Application.Dtos.AttachmentDto>()
                };

                foreach (var item in model.ListAttachments)
                {
                    byte[] data = new byte[] { };
                    using (MemoryStream stream = new MemoryStream())
                    {
                        item.CopyTo(stream);
                        data = stream.ToArray();
                        dto.OtherAttachments.Add(
                            new Application.Dtos.AttachmentDto
                            {
                                FileContentData = data,
                                FileName = item.FileName,
                            }
                        );
                    }
                }

                await _uploadRequestService.InsertAsync(dto);

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
