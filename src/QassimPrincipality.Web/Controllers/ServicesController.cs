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
namespace QassimPrincipality.Web.Controllers
{
    public class ServicesController : Controller
    {
       
        private readonly EServiceAppService _eService;
        private readonly NewsAppService _news;
		private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;

        public ServicesController(
            ILogger<HomeController> logger,
            IHtmlLocalizer<HomeController> localizer,
            EServiceAppService eService,
			NewsAppService news,
			StatisticAppService stats
			)
        {
            _logger = logger;
            _eService = eService;
            _localizer = localizer;
            _news = news;
        }

        public async Task<ActionResult> Index()
        {
            var services = await _eService.GetAll();
            ViewData["services"] = services;
			return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public async Task<ActionResult> Service(int Id)
        {
           // ViewData["NewsId"] = Id;

            var newsItem = await _news.GetByIdAsync(Id);
            ViewData["newsItem"] = newsItem;

            return View();
        }

       
    }
}
