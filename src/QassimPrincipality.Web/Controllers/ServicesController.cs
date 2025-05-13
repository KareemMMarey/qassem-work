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
namespace QassimPrincipality.Web.Controllers
{
    public class ServicesController : Controller
    {
       
        private readonly EServiceAppService _eService;
        private readonly LookupAppService _lookups;
        private readonly NewsAppService _news;
		private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;

        public ServicesController(
            ILogger<HomeController> logger,
            IHtmlLocalizer<HomeController> localizer,
            EServiceAppService eService,
			NewsAppService news,
            LookupAppService lookups
            )
        {
            _logger = logger;
            _eService = eService;
            _localizer = localizer;
            _news = news;
            _lookups = lookups;
        }

        public async Task<ActionResult> Index()
        {
            var categories = await _lookups.GetCategories();
            ViewData["categories"] = categories;
            var services = await _eService.GetAll();
            ViewData["services"] = services;
			return View();
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

        [HttpGet]
        public async Task<IActionResult> LoadStep(int serviceId, int stepNumber)
        {
            var service = await _eService.GetServiceStepsById(serviceId);
            var step = service.ServiceSteps.FirstOrDefault(step=>step.StepNumber== stepNumber);
            if (step == null)
                return NotFound("الخطوة غير موجودة");

            // اختر الـ Partial View بناءً على رقم الخطوة
            switch (step.StepNumber)
            {
                case 1:
                    return PartialView("_BasicInfoPartial");
                case 2:
                    return PartialView("_ContactInfoPartial");
                case 3:
                    return PartialView("_AttachmentPartial");
                case 4:
                    return PartialView("_ReviewPartial");
                default:
                    return PartialView("_NotFoundPartial");
            }
        }


    }
}
