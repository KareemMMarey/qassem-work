using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Dtos;

namespace eServices.Controllers
{
	public class EParticipationController : Controller
	{
		public IActionResult Index()
		{
			try
			{
				return View();
			}
			catch (Exception ex)
			{

				throw;
			}
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
        public IActionResult SubmitContact(ContactUsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("بيانات غير صالحة");
            }

            // أي منطق إرسال أو تخزين
            return Ok();
        }
    }
}
