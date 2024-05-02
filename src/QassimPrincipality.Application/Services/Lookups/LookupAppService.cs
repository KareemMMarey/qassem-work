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
        private readonly IRepository<ContactType> _contactTypeRepository;

        public LookupAppService(
                                IRepository<RequestType> requestTypeRepository,
                                IRepository<ContactType> contactTypeRepository
                                )
        {

            _requestTypeRepository = requestTypeRepository;
            _contactTypeRepository = contactTypeRepository;
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
        public async Task<List<SelectListItem>> GetConatctType()
        {
            return await _contactTypeRepository.TableNoTracking.Where(a => a.IsActive).Select(
                 s => new SelectListItem
                 {
                     Text = CultureHelper.IsArabic ? s.NameAr : s.NameEn,
                     Value = s.Id.ToString()
                 }
                 ).ToListAsync();
        }


    }
}