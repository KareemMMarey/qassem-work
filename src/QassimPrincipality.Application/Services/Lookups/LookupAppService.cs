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
        private readonly IRepository<EntityType> _entityRepository;
        private readonly IRepository<RequesterType> _requesterTypeRepository;

        public LookupAppService(
                                IRepository<RequestType> requestTypeRepository,
                                IRepository<ContactType> contactTypeRepository,
                                IRepository<EntityType> entityRepository,
                                IRepository<RequesterType> requesterTypeRepository
                                )
        {

            _requestTypeRepository = requestTypeRepository;
            _contactTypeRepository = contactTypeRepository;
            _entityRepository = entityRepository;
            _requesterTypeRepository = requesterTypeRepository;
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
        public async Task<List<SelectListItem>> GetEntities()
        {
            return await _entityRepository.TableNoTracking.Where(a => a.IsActive).Select(
                 s => new SelectListItem
                 {
                     Text = CultureHelper.IsArabic ? s.NameAr : s.NameEn,
                     Value = s.Id.ToString()
                 }
                 ).ToListAsync();



        }
        public async Task<List<SelectListItem>> GetRequesterTypes()
        {
            return await _requesterTypeRepository.TableNoTracking.Where(a => a.IsActive).Select(
                 s => new SelectListItem
                 {
                     Text = CultureHelper.IsArabic ? s.NameAr : s.NameEn,
                     Value = s.Id.ToString()
                 }
                 ).ToListAsync();



        }
    }
}