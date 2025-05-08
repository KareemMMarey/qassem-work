using Microsoft.AspNetCore.Mvc;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.NewShema;
using QassimPrincipality.Web.ViewModels.PageFeedback;

namespace QassimPrincipality.Web.Controllers
{
	public class PageFeedbackController : Controller
	{
		private readonly PageFeedbackAppService _feedbackService;

		public PageFeedbackController(PageFeedbackAppService feedbackService)
		{
			_feedbackService = feedbackService;
		}

		[HttpPost]
		public async Task<IActionResult> SubmitFeedback([FromBody] FeedBack feedBack)
		{
			var result = await _feedbackService.SubmitFeedbackAsync(feedBack.pageUrl, feedBack.isPositive);
			return Json(new { success = result });
		}

		[HttpGet]
		public async Task<IActionResult> GetAllFeedbacks()
		{
			var feedbacks = await _feedbackService.GetAllAsync();
			return Json(feedbacks);
		}

		[HttpGet]
		public async Task<IActionResult> GetFeedback(long id)
		{
			var feedback = await _feedbackService.GetByIdAsync(id);
			return Json(feedback);
		}

		[HttpGet]
		public async Task<IActionResult> SearchFeedbacks(PageFeedbackSearchDto searchDto)
		{
			var result = await _feedbackService.SearchAsync(searchDto);
			return Json(result);
		}
	}
}
