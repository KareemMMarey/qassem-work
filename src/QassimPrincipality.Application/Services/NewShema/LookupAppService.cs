using QassimPrincipality.Domain.Interfaces;
using Framework.Core.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using QassimPrincipality.Application.Dtos.Content;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Domain.Enums;
using Framework.Core.AutoMapper;

namespace QassimPrincipality.Application.Services.NewShema
{
    public class LookupAppService
    {


        private readonly IRepository<ServicesCategory> _servicesCategoryRepository;
        private readonly IRepository<LookupOption> _lookupOptionRepository;
        private readonly IRepository<Country> _countryRepository;
        public LookupAppService(
                                IRepository<ServicesCategory> servicesCategoryRepository,
                                IRepository<LookupOption> lookupOptionRepository,
                                IRepository<Country> countryRepository
                                )
        {
            _servicesCategoryRepository = servicesCategoryRepository;
            _lookupOptionRepository = lookupOptionRepository;
            _countryRepository = countryRepository;
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
        public async Task<List<LookupOptionDto>> GetPrisons()
        {
            var prisons = await _lookupOptionRepository.TableNoTracking.Where(c=>c.LookupOptionType== LookupOptionType.Prison).ToListAsync();
            return prisons.MapTo<List<LookupOptionDto>>();
        }
        public async Task<List<LookupOptionDto>> GetReasons()
        {
            var reasons = await _lookupOptionRepository.TableNoTracking.Where(c => c.LookupOptionType == LookupOptionType.ExitReasons).ToListAsync();
            return reasons.MapTo<List<LookupOptionDto>>();
        }
        public async Task<List<CountryDto>> GetCountries()
        {
            var countries = await _countryRepository.TableNoTracking.ToListAsync();
            return countries.MapTo<List<CountryDto>>();
        }

    }
}