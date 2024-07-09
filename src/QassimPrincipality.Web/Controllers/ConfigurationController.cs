using Framework.Core.Extensions;
using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.Lookups.Main.CommonEService;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceSubCategory;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Application.Services.Main.Evaluation;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.ViewModels.Category;
using QassimPrincipality.Web.ViewModels.Request;
using QassimPrincipality.Web.ViewModels.SubCategory;
using System.Text;

namespace QassimPrincipality.Web.Controllers
{
    [Authorize(Roles = "EServicesRequestAdmin,Admin")]
    public class ConfigurationController : Controller
    {
        private readonly ILogger<ConfigurationController> _logger;

        private readonly EServiceCategoryAppService _categoriesService;
        private readonly EServiceSubCategoryAppService _subCategoriesService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public ConfigurationController(ILogger<ConfigurationController> logger,
           EServiceCategoryAppService categoriesService,
           EServiceSubCategoryAppService subCategoriesService,
           UserManager<ApplicationUser> userManager,
           EvaluationAppService evaluationAppService, IWebHostEnvironment env)
        {
            _logger = logger;
            _categoriesService = categoriesService;
            _subCategoriesService = subCategoriesService;
            _userManager = userManager;
            _env = env;
        }
       
        public async Task<ActionResult> ServiceList()
        {
            ViewData["categories"] = await _categoriesService.GetAllEServiceCategories();
            return View();
        }
        public ActionResult CreateCategory()
        {
            return View();
        }
        public async Task<ActionResult> CreateSubCategory(int categoryId)
        {
            ViewData["category"] = await _categoriesService.GetById(categoryId);
            List<CustomSelectListItem> customItems = new List<CustomSelectListItem>
            {
            new CustomSelectListItem { Text = "المواطنون", Value = "المواطنون" },
            new CustomSelectListItem { Text = "المقيمون", Value = "المقيمون" },
            };
            ViewBag.CustomItems = customItems;
            return View(new ServiceSubCategoryVM());
        }

        public async Task<ActionResult> Activate(int serviceId,bool status)
        {
            try
            {
               
                var res = await _categoriesService.ChangeActiveStatus(serviceId,!status);
                return RedirectToAction("ServiceList");
            }
            catch
            {
                return RedirectToAction("ServiceList");
            }
        }
        // POST: RequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(ServiceCategoryVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                var dto = new CommonEServiceDto
                {
                    NameAr=model.NameAr,
                    DescriptionAr=model.DescriptionAr,
                    HasSubCategory = true,
                    Icon = _env.WebRootPath + "/images/service.png",
                    Url = "",
                    Audience = "",
                    CreatedBy = "E-k.marey",
                    CreatedOn = DateTime.Now,
                    ServiceFees = 0,
                    DurationDays = "حسب اتفاقية مستوى الخدمة",
                    ServiceRequierment = "",
                };
                var res = await _categoriesService.InsertAsync(dto);
                return RedirectToAction("ServiceList");
            }
            catch
            {
                return RedirectToAction("ServiceList");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSubCategory(ServiceSubCategoryVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<CustomSelectListItem> customItems = new List<CustomSelectListItem>
                    {
                    new CustomSelectListItem { Text = "المواطنون", Value = "المواطنون" },
                    new CustomSelectListItem { Text = "المقيمون", Value = "المقيمون" },
                    };
                    ViewBag.CustomItems = customItems;
                    return View(model);
                }
                var currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("");
                if(!string.IsNullOrEmpty(model.ServiceRequierment)) {
                    var list = model.ServiceRequierment.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (var item in list)
                    {
                        sb.Append("<li>" + item + "</li>");
                    }
                }
               

                var dto = new CommonEServiceDto
                {
                    NameAr = model.NameAr,
                    Icon = _env.WebRootPath + "/images/service.png",
                    Url = "",
                    Audience = model.Audience,
                    CreatedBy = currentUser.UserName,
                    CreatedOn = DateTime.Now,
                    ServiceFees = model.ServiceFees,
                    DurationDays = model.DurationDays,
                    DescriptionAr =model.DescriptionAr,
                    ServiceRequierment = sb.ToString(),
                    CategoryId = model.CategoryId,
                };
                var res = await _subCategoriesService.InsertAsync(dto);
                return RedirectToAction("ServiceList");
            }
            catch
            {
                return RedirectToAction("ServiceList");
            }
        }

        // GET: RequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
