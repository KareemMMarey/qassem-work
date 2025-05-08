using QassimPrincipality.Domain.Interfaces;
using Framework.Core.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;

namespace QassimPrincipality.Application.Services.NewShema
{
    public class LookupAppService
    {


        private readonly IRepository<ServicesCategory> _servicesCategoryRepository;
        public LookupAppService(
                                IRepository<ServicesCategory> servicesCategoryRepository
                                )
        {
            _servicesCategoryRepository = servicesCategoryRepository;
        }


        public async Task<List<SelectListItem>> GetCategories()
        {
            return await _servicesCategoryRepository.TableNoTracking.Where(a => a.IsActive).Select(
                 s => new SelectListItem
                 {
                     Text = CultureHelper.IsArabic ? s.NameAr : s.NameEn,
                     Value = s.Id.ToString()
                 }
                 ).ToListAsync();
        }
       
    }
}