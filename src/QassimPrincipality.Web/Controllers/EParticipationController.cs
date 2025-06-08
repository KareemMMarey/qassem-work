using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.Lookups.Main.SharedContactFormService;

namespace eServices.Controllers
{
    public class EParticipationController : Controller
    {
        private readonly SharedContactFormAppService _sharedContactFormAppService;

        public EParticipationController(SharedContactFormAppService sharedContactFormAppService)
        {
            _sharedContactFormAppService = sharedContactFormAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ESurveys()
        {
            return View();
        }

        public IActionResult BeneficiaryVoice()
        {
            return View();
        }

        public IActionResult Suggestions()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult OpenDataRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContact(ContactUsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("بيانات غير صالحة");
            }

            await _sharedContactFormAppService.InsertAsync(model);

            return Ok();
        }
        
    }
}
