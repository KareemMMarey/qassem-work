using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceSubCategory;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Application.Services.Main.Evaluation;
using QassimPrincipality.Web.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;
namespace QassimPrincipality.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EServiceCategoryAppService _categoriesService;
        private readonly EServiceSubCategoryAppService _subCategoriesService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EvaluationAppService _evaluationAppService;


        public HomeController(ILogger<HomeController> logger,
            EServiceCategoryAppService categoriesService,
            EServiceSubCategoryAppService subCategoriesService,
            UserManager<ApplicationUser> userManager,
            EvaluationAppService evaluationAppService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
            _subCategoriesService = subCategoriesService;
            _userManager = userManager;
            _evaluationAppService = evaluationAppService;
        }

        public async Task<ActionResult> Index()
        {
            ViewData["categories"] = await _categoriesService.GetActiveEServiceCategories();
            _logger.LogInformation("Executing Index action.");
            //throw new Exception("Test exception");
            return View();
        }
        public ActionResult About()
        {
            
            return View();
        }
        public ActionResult MyRequests()
        {
            
            return View();
        }
        public async Task<ActionResult> SubCategories(int? categoryId)
        {
            ViewData["subcategories"] = await _subCategoriesService.GetActiveEServiceSubCategories(categoryId);
            return View();
        }
        [Authorize]
        public async Task<ActionResult> ServiceDefination(int id,bool isCategory, string message="")
        {
            var currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ViewBag.UserName = currentUser.UserName;
            ViewBag.ServiceId = id;

            var checkInserted = await _evaluationAppService.CheckExist(id, currentUser.UserName);
            if (checkInserted.Count > 0)
                ViewBag.IsEvaluationDone = "True";
            else ViewBag.IsEvaluationDone = "False";

            var allEvaluations = await _evaluationAppService.GetAllEvaluationsByService(id);
            if (allEvaluations.Count > 0)
            {
                ViewBag.AllEvaluations = allEvaluations.Count;
                decimal res = allEvaluations.Sum(a => a.EvalutionValue) / allEvaluations.Count;
                ViewBag.EvaluationAvarege = Math.Truncate(res);

            }
            else
            {
                ViewBag.EvaluationAvarege = 0;
                ViewBag.AllEvaluations = 0;
            }

            if (isCategory) {
                ViewData["service"] = await _categoriesService.GetById(id);
            }
            else {
                ViewData["service"] = await _subCategoriesService.GetById(id);
            }
            if(!string.IsNullOrEmpty(message)) {
                ViewBag.AddValuationErrorMessage=message;
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> AddEvaluation(string userName, int serviceId, int eveluationValue)
        {
            EvaluationDto evaluationDto = new EvaluationDto();
            var checkInserted = await _evaluationAppService.CheckExist(serviceId, userName);
            string message = "";
            if (eveluationValue > 0&& checkInserted.Count==0)
            {
                evaluationDto.SubCategoryId = serviceId;
                evaluationDto.EvalutionValue = eveluationValue;
                evaluationDto.CreatedBy = userName;
                evaluationDto.CreatedOn=DateTime.Now;
               var result =  await _evaluationAppService.InsertAsync(evaluationDto);

            }
            else {
                if(checkInserted.Count>0)
                    message = "تم التقييم من قبل ";
                else
                    message = "NotValid";
            }
            return RedirectToAction("ServiceDefination",new { id = serviceId, isCategory = false, message= message });

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
