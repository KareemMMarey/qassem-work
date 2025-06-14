using Framework.Core.AutoMapper;
using Framework.Core.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using QassimPrincipality.Domain.Enums;
using QassimPrincipality.Domain.Interfaces;

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

        public async Task<List<LookupOptionDto>> GetAllAsync()
        {
            var entities = await _lookupOptionRepository.TableNoTracking.ToListAsync();
            return entities.MapTo<List<LookupOptionDto>>();
        }

        public async Task<List<SelectListItem>> GetCategories()
        {
            return await _servicesCategoryRepository
                .TableNoTracking.Where(a => a.IsActive)
                .Select(s => new SelectListItem
                {
                    Text = CultureHelper.IsArabic ? s.NameAr : s.NameEn,
                    Value = s.Id.ToString(),
                })
                .ToListAsync();
        }

        public async Task<List<LookupOptionDto>> GetPrisons()
        {
            var prisons = await _lookupOptionRepository
                .TableNoTracking.Where(c => c.LookupOptionType == LookupOptionType.Prison)
                .ToListAsync();
            return prisons.MapTo<List<LookupOptionDto>>();
        }

        public async Task<List<LookupOptionDto>> GetReasons()
        {
            var reasons = await _lookupOptionRepository
                .TableNoTracking.Where(c => c.LookupOptionType == LookupOptionType.ExitReasons)
                .ToListAsync();
            return reasons.MapTo<List<LookupOptionDto>>();
        }

        public async Task<List<CountryDto>> GetCountries()
        {
            var countries = await _countryRepository.TableNoTracking.ToListAsync();
            return countries.MapTo<List<CountryDto>>();
        }

        public async Task<LookupOptionDto> GetByIdAsync(int id)
        {
            var entity = await _lookupOptionRepository.TableNoTracking.FirstOrDefaultAsync(x =>
                x.Id == id
            );
            return entity == null ? null : entity.MapTo<LookupOptionDto>();
        }

        public async Task<LookupOptionDto> InsertAsync(LookupOptionDto dto)
        {
            var entity = dto.MapTo<LookupOption>();
            await _lookupOptionRepository.InsertAsync(entity, true);

            return entity.MapTo<LookupOptionDto>();
        }

        public async Task UpdateAsync(LookupOptionDto dto)
        {
            var entity = await _lookupOptionRepository.Table.FirstOrDefaultAsync(x =>
                x.Id == dto.Id
            );
            if (entity == null)
                return;

            entity = dto.MapTo<LookupOption>();
            await _lookupOptionRepository.UpdateAsync(entity, true);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _lookupOptionRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return;

            await _lookupOptionRepository.DeleteAsync(entity, true);
        }
    }
}
