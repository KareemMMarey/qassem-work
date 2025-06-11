using System.Security.Claims;
using Framework.Core.Extensions;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Newtonsoft.Json;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.NewShema;
using QassimPrincipality.Application.Services.NewShema.Content;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.ViewModels.Inquery;
using QassimPrincipality.Web.ViewModels.Request;
using System.Globalization;
namespace QassimPrincipality.Web.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly EServiceAppService _eService;
        private readonly ServiceRequestAppService _serviceRequestAppService;
        private readonly LookupAppService _lookups;
        private readonly NewsAppService _news;
        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        UserManager<ApplicationUser> _userManager;
        private readonly LogAppService _logservice;
        private readonly UserAppService _userAppService;

        public RequestController(
            ILogger<HomeController> logger,
            IHtmlLocalizer<HomeController> localizer,
            EServiceAppService eService,
            NewsAppService news,
            LookupAppService lookups,
            IWebHostEnvironment environment,
            ServiceRequestAppService serviceRequestAppService,
            UserManager<ApplicationUser> userManager,
            LogAppService logservice,
            UserAppService userAppService
        )
        {
            _logger = logger;
            _eService = eService;
            _localizer = localizer;
            _news = news;
            _lookups = lookups;
            _environment = environment;
            _serviceRequestAppService = serviceRequestAppService;
            _userManager = userManager;
            _logservice = logservice;
            _userAppService = userAppService;
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid requestId)
        {
            try
            {
                var request = await _serviceRequestAppService.GetRequestByIdAsync(requestId);
                ViewBag.UserId = HttpContext
                    .User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                    .Value;
                return View(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving request: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRequestStatus(
            [FromForm] ChangeStatusDto changeStatusDto
        )
        {
            try
            {
                var UserId = User
                    .Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                    ?.Value;
                var xx = Guid.Parse(changeStatusDto.RequestId);

                await _serviceRequestAppService.ChangeRequestStatusAsync(
                    Guid.Parse(changeStatusDto.RequestId),
                    changeStatusDto.NewStatus,
                    UserId,
                    changeStatusDto.ActionNotes
                );
                return RedirectToAction("AllRequests");
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
                return View("MyRequests", results); // Pass list to the view
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching requests: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                RequestSearchFilterDto filter = new RequestSearchFilterDto
                {
                    UserId = user.Id.ToString(),
                };
                var results = await _serviceRequestAppService.SearchRequestsAsync(filter);

                return View(
                    new MyRequestsViewModel
                    {
                        Filter = new RequestSearchFilterDto(),
                        Results = results,
                    }
                );

                //return View(results); // Pass list to the view
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching requests: {ex.Message}");
            }
        }

        public ServiceRequestAppService Get_serviceRequestAppService()
        {
            return _serviceRequestAppService;
        }

        [HttpGet]
        public async Task<IActionResult> AllRequests([FromQuery] RequestSearchFilterDto filter)
        {
            try
            {
                var results = await _serviceRequestAppService.SearchRequestsAsync(filter);

                var services = await _serviceRequestAppService.GetServices();

                return View(
                    new MyRequestsViewModel
                    {
                        Filter = filter ?? new RequestSearchFilterDto(),
                        Results = results,
                        ServiceList = services,
                    }
                );

                //return View(results); // Pass list to the view
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching requests: {ex.Message}");
            }
        }

        [HttpPost]
            public async Task<IActionResult> MyRequests(RequestSearchFilterDto filter)
        {
            try
            {
                var results = await _serviceRequestAppService.SearchRequestsAsync(filter);
                return View(new MyRequestsViewModel { Filter = filter, Results = results });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching requests: {ex.Message}");
            }
        }

        //[HttpGet]
        //public async Task<ActionResult> LoadStep(int serviceId, int stepNumber)
        //{
        //    var serviceWithSteps = await _eService.GetServiceStepsById(serviceId);
        //    var step = serviceWithSteps.ServiceSteps.FirstOrDefault(s => s.StepNumber == stepNumber);
        //    if (step == null)
        //        return NotFound("Step not found");

        //    return PartialView($"_{step.NameEn.Replace(" ", "")}Partial", step);
        //}
        [HttpGet]
        public async Task<ActionResult> LoadStep(int serviceId, int stepNumber)
        {
            // Load the full EService with steps and attachments
            var serviceWithSteps = await _eService.GetServiceStepsById(serviceId);
            ViewBag.HasApplicantStatus = serviceWithSteps.HasApplicantStatus;
            ViewBag.HasTypeOfSummons = serviceWithSteps.HasTypeOfSummons;
            if (serviceWithSteps == null)
                return NotFound("Service not found");

            // Find the requested step
            var step = serviceWithSteps.ServiceSteps.FirstOrDefault(s =>
                s.StepNumber == stepNumber
            );
            if (step == null)
                return NotFound("Step not found");

            // Check if the step is for attachments
            if (step.NameEn.Equals("Attachments", StringComparison.OrdinalIgnoreCase))
            {
                // Pass the full EService model to the attachments partial
                return PartialView("_AttachmentsPartial", serviceWithSteps.AttachmentTypes);
            }

            if (step.NameEn.Equals("Request Subject Info", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Nationalities = await _lookups.GetCountries();
                ViewBag.Prisons = await _lookups.GetPrisons();
                ViewBag.Reasons = await _lookups.GetReasons();
                ViewBag.ServiceCategoryId = serviceWithSteps.CategoryId;
                ViewBag.ServiceId = serviceWithSteps.Id;
            }

            // Load regular step partial
            return PartialView($"_{step.NameEn.Replace(" ", "")}Partial", step);
        }

        [HttpPost]
        public async Task<IActionResult> SaveStepData(
            Guid requestId,
            int serviceId,
            int stepNumber,
            string stepData,
            string userId
        )
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (stepNumber == 1)
                {
                    var basicDataDto =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<RequestBasicDataDto>(
                            stepData
                        );
                    var initiateRequest = await _serviceRequestAppService.CreateDraftRequestAsync(
                        new CreateServiceRequestDto
                        {
                            Id = Guid.NewGuid(),
                            ServiceId = serviceId,
                            UserId = user.Id.ToString(),
                            ServiceRequesterRelation = basicDataDto.ServiceRequesterRelation,
                        }
                    );
                    await _serviceRequestAppService.SaveBasicDataAsync(
                        initiateRequest.Id,
                        basicDataDto,
                        userId
                    );
                    requestId = initiateRequest.Id;
                    ViewBag.RequestId = requestId;
                }
                else
                {
                    var additionalDataDto =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<RequestAdditionalDataDto>(
                            stepData
                        );
                    await _serviceRequestAppService.SaveAdditionalDataAsync(
                        requestId,
                        additionalDataDto,
                        userId
                    );
                    //ViewBag.RequestId = requestId;
                }

                return Ok(new { success = true, requestId = requestId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving step data: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttachments(
            string userId,
            Guid requestId,
            int attachmentTypeId,
            List<IFormFile> files
        )
        {
            try
            {
                var userName = User.Identity.Name;

                if (files == null || files.Count == 0)
                    return BadRequest("No files provided");

                // Create a directory for this specific request
                var requestFolder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    requestId.ToString()
                );
                Directory.CreateDirectory(requestFolder);

                var uploadedFiles = new List<RequestAttachmentDto>();

                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                        continue;

                    // Generate a unique file name to avoid conflicts
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                    var filePath = Path.Combine(requestFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var attachmentDto = new RequestAttachmentDto
                    {
                        AttachmentTypeId = attachmentTypeId,
                        FileName = fileName,
                        FilePath = filePath,
                        IsValid = false,
                    };

                    // Save the attachment in the database
                    await _serviceRequestAppService.AddAttachmentAsync(
                        requestId,
                        attachmentDto,
                        userId
                    );
                    uploadedFiles.Add(attachmentDto);
                }

                // Return the list of uploaded file names
                return Ok(uploadedFiles.Select(a => new { success = true, fileName = a.FileName }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading files: {ex.Message}");
            }
        }
                
        [HttpPost]
        public async Task<IActionResult> DeleteAttachment(string requestId,string fileName)
        {
            try
            {
                    // Save the attachment in the database
                    await _serviceRequestAppService.DeleteAttachmentAsync(new Guid(requestId),fileName);

                // Return the list of uploaded file names
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading files: {ex.Message}");
            }
        }



        [HttpPost]
        public IActionResult UploadAttachment(List<IFormFile> files)
        {
            var uploadedFiles = new List<object>();

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                uploadedFiles.Add(new { fileName });
            }

            return Json(uploadedFiles);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequest(Guid requestId)
        {
            try
            {
                if (requestId == Guid.Empty)
                    requestId = Guid.Parse("ceadbb51-1c0b-46d3-b27a-bf2d05788c09");
                var request = await _serviceRequestAppService.GetRequestByIdAsync(requestId);
                if (request == null)
                    return BadRequest("Request not found");

                var result = await _serviceRequestAppService.SubmitRequestAsync(
                    requestId,
                    "current-user"
                );
                return Ok(new { success = true, requestNumber = request.RequestNumber , requestId = request.Id});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error submitting request: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult Success(string requestNumber, string requestId)
        {
            if (string.IsNullOrEmpty(requestNumber))
            {
                // If no request number, redirect to a more appropriate page
                return RedirectToAction("Index", "Home");
            }

            // Pass the request number to the view
            ViewBag.RequestNumber = requestNumber;
            ViewBag.requestId = requestId;
            return View();
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
                var user = await _userManager.FindByNameAsync(User?.Identity?.Name);

                if(user == null)
                {
                    return StatusCode(500, $"Error retrieving user data: No User Founded");

                }
                // Simulated user data (replace this with actual user data retrieval logic)
                var userData = new
                {
                    fullName = /*"عبدالله احمد محمد الأحمد"*/ user.FullName,
                    nationality = /*"المملكة العربية السعودية"*/ user.Nationality,
                    birthDate = /*"1998-05-19"*/ user.DateOfBirth.HasValue ? user.DateOfBirth.Value.ToString("yyyy-MM-dd", new CultureInfo("en-US")) : "",
                    idNumber = /*"1109882374"*/ user.IdentityNumber,
                    email = /*"example@mail.com"*/ user.Email,
                    phone = /*"0555555555"*/ user.PhoneNumber,
                    city = /*"بريدة"*/ user.City,
                    district = /*"حي الصفراء"*/ user.Neighborhood
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

        [HttpGet]
        public async Task<ActionResult> InqueryTransaction(int serviceId)
        {
            try
            {
                var serviceWithSteps = await _eService.GetServiceStepsById(serviceId);

                InqueryVM vM = new InqueryVM();

                vM.NameAr = serviceWithSteps.NameAr;
                vM.NameEn = serviceWithSteps.NameEn;
                var user = await _userAppService.GetUserAsync(Guid.Parse(HttpContext.User.GetId()));
                //vM.UserFullName = user.FullNameAr ?? user.FullName;
                vM.NationalNo = user.UserName.Replace("@nafath", "");
                return View(vM);
            }
            catch (Exception ex)
            {
                await _logservice.InsertAsync(
                    new Framework.Core.SharedServices.Dto.LogDto
                    {
                        CallSite = "",
                        Date = DateTime.Now,
                        Exception = "Inquery Index Catch" + ex.ToString(),
                        Host = "myhost",
                        Logger = "my logger",
                        LogLevel = "Info",
                        MachineName = "manual",
                        Message = "my message",
                        Thread = "No thread",
                        Url = "api",
                        UserAgent = "agent",
                        UserName = "my name",
                        Id = Guid.NewGuid(),
                    }
                );
                return View(new InqueryVM());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InqueryTransaction(InqueryVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "حدث خطأ أثناء عملية الاستعلام";
                return View(model);
            }
            try
            {
                var result = await ApiConsumer.ServiceConsumerAsync<dynamic>(
                    "https://dms.alqassim.gov.sa/INTEGSERV_DEMO/api/Receiver/SearchForRecord/?RecordNo="
                        + long.Parse(model.RecordNo)
                        + "&NationalNo="
                        + long.Parse(model.NationalNo)
                );
                await _logservice.InsertAsync(
                    new Framework.Core.SharedServices.Dto.LogDto
                    {
                        CallSite = "",
                        Date = DateTime.Now,
                        Exception =
                            "Header data "
                            + JsonConvert.SerializeObject(result, Formatting.Indented),
                        Host = "myhost",
                        Logger = "my logger",
                        LogLevel = "Info",
                        MachineName = "manual",
                        Message = "my message",
                        Thread = "No thread",
                        Url = "api",
                        UserAgent = "agent",
                        UserName = "my name",
                        Id = Guid.NewGuid(),
                    }
                );

                if (result["isSuccess"].ToString() == "True")
                {
                    var RequestStatus = result["RequestStatus"].ToString();
                    ViewBag.RequestStatus = RequestStatus;
                }
                else
                {
                    ViewBag.RequestStatus = "لا توجد بيانات ";
                }
            }
            catch (Exception ex)
            {
                await _logservice.InsertAsync(
                    new Framework.Core.SharedServices.Dto.LogDto
                    {
                        CallSite = "",
                        Date = DateTime.Now,
                        Exception = "InqueryRequest" + ex.ToString(),
                        Host = "myhost",
                        Logger = "my logger",
                        LogLevel = "Info",
                        MachineName = "manual",
                        Message = "my message",
                        Thread = "No thread",
                        Url = "api",
                        UserAgent = "agent",
                        UserName = "my name",
                        Id = Guid.NewGuid(),
                    }
                );
                ViewBag.ErrorMessage = "حدث خطأ أثناء عملية الاستعلام";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PreviewAttachment(Guid requestId, int attachmentId)
        {
            // Example: fetch file info from DB/service using requestId and attachmentId
            var attachment = await _serviceRequestAppService.GetAttachment(requestId, attachmentId);
            if (attachment == null || string.IsNullOrEmpty(attachment.ContentType) || attachment.Data == null)
            {
                return NotFound("Attachment not found.");
            }

            // Return the file for preview
            return File(attachment.Data, attachment.ContentType);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadAttachment(Guid requestId, int attachmentId)
        {
            // Fetch the attachment (same service as used in preview)
            var attachment = await _serviceRequestAppService.GetAttachment(requestId, attachmentId);
            if (attachment == null || string.IsNullOrEmpty(attachment.ContentType) || attachment.Data == null)
            {
                return NotFound("Attachment not found.");
            }

            // Use a default or meaningful file name
            var fileName = string.IsNullOrEmpty(attachment.FileName)
                ? $"attachment_{attachmentId}{GetFileExtension(attachment.ContentType)}"
                : attachment.FileName;

            // Return as downloadable file
            return File(attachment.Data, attachment.ContentType, fileName);
        }

        private string GetFileExtension(string contentType)
        {
            return contentType switch
            {
                "application/pdf" => ".pdf",
                "image/png" => ".png",
                "image/jpeg" => ".jpg",
                "application/msword" => ".doc",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ".docx",
                "application/vnd.ms-excel" => ".xls",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => ".xlsx",
                _ => ""
            };
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
