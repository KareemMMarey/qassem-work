namespace QassimPrincipality.Web.Controllers
{
    using Framework.Core.AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using QassimPrincipality.Application.Dtos;
    using QassimPrincipality.Application.Services.NewShema;
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin")]
    public class LookupOptionController : Controller
    {
        private readonly LookupAppService _lookupOptionService;

        public LookupOptionController(LookupAppService lookupOptionService)
        {
            _lookupOptionService = lookupOptionService;
        }

        // GET: /LookupOption
        public async Task<IActionResult> Index()
        {
            var list = await _lookupOptionService.GetAllAsync();
            return View(list);
        }

        // GET: /LookupOption/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var entity = await _lookupOptionService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        // GET: /LookupOption/Create
        public IActionResult Create()
        {
            return View(new LookupOptionDto());
        }

        // POST: /LookupOption/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LookupOptionDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _lookupOptionService.InsertAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /LookupOption/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _lookupOptionService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            var dto = entity.MapTo<LookupOptionDto>(); // Or manual mapping
            return View(dto);
        }

        // POST: /LookupOption/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LookupOptionDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            await _lookupOptionService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /LookupOption/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _lookupOptionService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        // POST: /LookupOption/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _lookupOptionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
