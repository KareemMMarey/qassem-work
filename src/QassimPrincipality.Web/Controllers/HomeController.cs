using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceSubCategory;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;

using QassimPrincipality.Web.Models;
using System.Diagnostics;

namespace QassimPrincipality.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EServiceCategoryAppService _categoriesService;
        private readonly EServiceSubCategoryAppService _subCategoriesService;



        public HomeController(ILogger<HomeController> logger, 
            EServiceCategoryAppService categoriesService,
            EServiceSubCategoryAppService subCategoriesService
            )
        {
            _logger = logger;
            _categoriesService = categoriesService;
            _subCategoriesService = subCategoriesService;
        }

        public async Task<ActionResult> Index()
        {
            ViewData["categories"] = await _categoriesService.GetAllEServiceCategories();
            return View();
        }
        public async Task<ActionResult> SubCategories(int? categoryId)
        {
            ViewData["subcategories"] = await _subCategoriesService.GetAllEServiceSubCategories(categoryId);
            return View();
        }
        public async Task<ActionResult> ServiceDefination(int id,bool isCategory)
        {
            if (isCategory) {
                ViewData["service"] = await _categoriesService.GetById(id);
            }
            else {
                ViewData["service"] = await _subCategoriesService.GetById(id);
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
