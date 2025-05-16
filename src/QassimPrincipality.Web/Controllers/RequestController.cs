using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceSubCategory;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Application.Services.Main.Evaluation;
using QassimPrincipality.Web.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using QassimPrincipality.Application.Services.NewShema.Content;
using QassimPrincipality.Application.Services.NewShema;
using QassimPrincipality.Domain.Entities.Services.NewSchema;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Domain.Enums;
using QassimPrincipality.Application.Dtos;
namespace QassimPrincipality.Web.Controllers
{
    public class RequestController : Controller
    {
       
        private readonly EServiceAppService _eService;
        private readonly ServiceRequestAppService _serviceRequestAppService;
        private readonly LookupAppService _lookups;
        private readonly NewsAppService _news;
		private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public RequestController(
            ILogger<HomeController> logger,
            IHtmlLocalizer<HomeController> localizer,
            EServiceAppService eService,
			NewsAppService news,
            LookupAppService lookups,
            IWebHostEnvironment environment,
            ServiceRequestAppService serviceRequestAppService
            )
        {
            _logger = logger;
            _eService = eService;
            _localizer = localizer;
            _news = news;
            _lookups = lookups;
            _environment = environment;
            _serviceRequestAppService = serviceRequestAppService;

        }

   
        public async Task<ActionResult> Index(int serviceId)
        {
            var serviceWithSteps = await _eService.GetServiceStepsById(serviceId);
            ViewBag.HasApplicantStatus = serviceWithSteps.HasApplicantStatus;
            ViewBag.HasTypeOfSummons = serviceWithSteps.HasTypeOfSummons;
            return View(serviceWithSteps);
        }

        [HttpGet]
        public async Task<IActionResult> GetRequestById(Guid requestId)
        {
            try
            {
                var request = await _serviceRequestAppService.GetRequestByIdAsync(requestId);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving request: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRequestStatus([FromBody] ChangeStatusDto changeStatusDto)
        {
            try
            {
                await _serviceRequestAppService.ChangeRequestStatusAsync(
                    changeStatusDto.RequestId,
                    changeStatusDto.NewStatus,
                    changeStatusDto.UserId,
                    changeStatusDto.ActionNotes
                );
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error changing request status: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SearchRequests([FromBody] RequestSearchFilterDto filter)
        {
            try
            {
                var results = await _serviceRequestAppService.SearchRequestsAsync(filter);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching requests: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<ActionResult> LoadStep(int serviceId, int stepNumber)
        {
            var serviceWithSteps = await _eService.GetServiceStepsById(serviceId);
            var step = serviceWithSteps.ServiceSteps.FirstOrDefault(s => s.StepNumber == stepNumber);
            if (step == null)
                return NotFound("Step not found");

            return PartialView($"_{step.NameEn.Replace(" ", "")}Partial", step);
        }

        [HttpPost]
        public async Task<IActionResult> SaveStepData(Guid requestId, int stepNumber, string stepData, string userId)
        {
            try
            {
                if (stepNumber == 1)
                {
                    var basicDataDto = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestBasicDataDto>(stepData);
                    await _serviceRequestAppService.SaveBasicDataAsync(requestId, basicDataDto, userId);
                }
                else
                {
                    var additionalDataDto = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestAdditionalDataDto>(stepData);
                    await _serviceRequestAppService.SaveAdditionalDataAsync(requestId, additionalDataDto, userId);
                }

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving step data: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttachment(Guid requestId, IFormFile file, string userId)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Invalid file");

                var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, file.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var attachmentDto = new RequestAttachmentDto
                {
                    FileName = file.FileName,
                    FilePath = filePath,
                    IsValid = false
                };

                await _serviceRequestAppService.AddAttachmentAsync(requestId, attachmentDto, userId);

                return Ok(new { success = true, fileName = file.FileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading file: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequest(Guid requestId, string userId)
        {
            try
            {
                var result = await _serviceRequestAppService.SubmitRequestAsync(requestId, userId);
                return Ok(new { success = true, requestId = result.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error submitting request: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttachment(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadPath);
            var filePath = Path.Combine(uploadPath, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Json(new { success = true, fileName = file.FileName });
        }
        public IActionResult About()
        {
            return View();
        }
        public async Task<ActionResult> AboutTheService(int Id)
        {
           // ViewData["NewsId"] = Id;

            var serviceItem = await _eService.GetServiceDetailsById(Id);
            ViewData["serviceItem"] = serviceItem;

            return View();
        }
        public async Task<ActionResult> Steps(int serviceId)
        {
            // جلب الخطوات من قاعدة البيانات
            var service = await _eService.GetServiceStepsById(serviceId);
            ViewBag.TotalSteps = service.ServiceSteps.Count;
            ViewBag.ServiceId = serviceId;
            return View(service.ServiceSteps);
        }
        // Simulate user data for now (replace this with real data from your database)
        [HttpGet]
        public async Task<IActionResult> GetUserData()
        {
            try
            {
                // Simulated user data (replace this with actual user data retrieval logic)
                var userData = new
                {
                    fullName = "عبدالله احمد محمد الأحمد",
                    nationality = "المملكة العربية السعودية",
                    birthDate = "1998-05-19",
                    idNumber = "1109882374",
                    email = "example@mail.com",
                    phone = "0555555555",
                    city = "بريدة",
                    district = "حي الصفراء"
                };

                // Return the data as JSON
                return Json(userData);
            }
            catch (Exception ex)
            {
                // Return an error response
                return StatusCode(500, $"Error retrieving user data: {ex.Message}");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> LoadStep(int serviceId, int stepNumber)
        //{
        //    var service = await _eService.GetServiceStepsById(serviceId);
        //    var step = service.ServiceSteps.FirstOrDefault(step=>step.StepNumber== stepNumber);
        //    if (step == null)
        //        return NotFound("الخطوة غير موجودة");

        //    // اختر الـ Partial View بناءً على رقم الخطوة
        //    switch (step.StepNumber)
        //    {
        //        case 1:
        //            return PartialView("_BasicInfoPartial");
        //        case 2:
        //            return PartialView("_ContactInfoPartial");
        //        case 3:
        //            return PartialView("_AttachmentPartial");
        //        case 4:
        //            return PartialView("_ReviewPartial");
        //        default:
        //            return PartialView("_NotFoundPartial");
        //    }
        //}


    }
}
