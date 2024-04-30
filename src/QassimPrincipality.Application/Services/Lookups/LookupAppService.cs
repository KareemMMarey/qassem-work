using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace QassimPrincipality.Application.Lookups.Services
{
    public class LookupAppService
    {
       
        private readonly IRepository<RequestType> _requestTypeRepository;

        public LookupAppService(
                                IRepository<RequestType> requestTypeRepository
                                )
        {

            _requestTypeRepository = requestTypeRepository;
        }

        

        public async Task<List<SelectListItem>> GetRequestType()
        {
            return await _requestTypeRepository.TableNoTracking.Where(a => a.IsActive).Select(
                 s => new SelectListItem
                 {
                     Text = CultureHelper.IsArabic ? s.NameAr : s.NameEn,
                     Value = s.Id.ToString()
                 }
                 ).ToListAsync();
        }

        
    }
}