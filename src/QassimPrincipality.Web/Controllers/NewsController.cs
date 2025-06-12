using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QassimPrincipality.Application.Dtos.Content;
using QassimPrincipality.Application.Services.NewShema.Content;
using QassimPrincipality.Web.ViewModels.News;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Globalization;

public class NewsController : Controller
{
    private readonly NewsAppService _newsService;
    private readonly IWebHostEnvironment _environment;

    public NewsController(NewsAppService newsService, IWebHostEnvironment environment)
    {
        _newsService = newsService;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            NewsVM data = new NewsVM();
            var results = await _newsService.GetAllAsync();
            data.Results = results;
            return View(data); // عرض القائمة
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error loading news: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> AddEdit(int? id)
    {
        var model = id.HasValue ? await _newsService.GetByIdAsync(id.Value) : new NewsDto() { PublishDate=DateTime.Now};
        model.PublishDateString = model.PublishDate.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddEdit(int? id, NewsDto news, IFormFile? file)
    {
        try
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;

            if (string.IsNullOrWhiteSpace(news.NameAr))
            {
                ModelState.AddModelError(nameof(news.NameAr), currentCulture == "ar-SA" ? "العنوان (عربي) مطلوب" : "Title (Arabic) is required.");
            }

            if (string.IsNullOrWhiteSpace(news.NameEn))
            {
                ModelState.AddModelError(nameof(news.NameEn), currentCulture == "ar-SA" ? "العنوان (إنجليزي) مطلوب" : "Title (English) is required.");
            }

            if (string.IsNullOrWhiteSpace(news.ShortDescriptionAr))
            {
                ModelState.AddModelError(nameof(news.ShortDescriptionAr), currentCulture == "ar-SA" ? "الوصف (عربي) مطلوب" : "Description (Arabic) is required.");
            }

            if (string.IsNullOrWhiteSpace(news.ShortDescriptionEn))
            {
                ModelState.AddModelError(nameof(news.ShortDescriptionEn), currentCulture == "ar-SA" ? "الوصف (إنجليزي) مطلوب" : "Description (English) is required.");
            }

            if (news.Id == 0 && file == null)
            {
                ModelState.AddModelError("file", currentCulture == "ar-SA" ? "صورة الخبر مطلوبة" : "Image is required.");
            }

            if (!ModelState.IsValid)
                return View(news);
            
            if (file == null && (id == null || id == 0))
                return View(news);

            var userName = User.Identity.Name;

            if (!string.IsNullOrEmpty(news.PublishDateString) && !string.IsNullOrWhiteSpace(news.PublishDateString))
            {
                DateTime date = DateTime.ParseExact(news.PublishDateString, "yyyy-MM-dd", new CultureInfo("en-US"));
                news.PublishDate = date;//Convert.ToDateTime(news.PublishDateString);
            }
            // رفع الصورة إذا تم توفيرها
            if (file != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "news");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                news.ImageUrl = "/uploads/news/" + fileName;

                // توليد Thumbnail
                var thumbFolder = Path.Combine(uploadsFolder, "thumbs");
                Directory.CreateDirectory(thumbFolder);

                var thumbFile = "thumb_" + fileName;
                var thumbPath = Path.Combine(thumbFolder, thumbFile);

                using (var image = await Image.LoadAsync(fullPath))
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(300, 0),
                        Mode = ResizeMode.Max
                    }));
                    await image.SaveAsync(thumbPath, new JpegEncoder());
                }

                news.ImageThumbnailUrl = "/uploads/news/thumbs/" + thumbFile;
            }

            if (id == null || id == 0)
            {
                news.CreatedBy = userName;
                await _newsService.InsertAsync(news);
            }
            else
            {
                news.UpdatedBy = userName;
                await _newsService.UpdateAsync(news);
            }

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error saving news: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _newsService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error deleting news: {ex.Message}");
        }
    }
}
