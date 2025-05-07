using Framework.Core.AutoMapper;
using Framework.Core.SharedServices.Services;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Application.Dtos.Content;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content;
using QassimPrincipality.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.NewShema.Content
{
    public class StatisticAppService
    {
        private readonly IRepository<Statistic> _statisticRepository;
        private readonly AppSettingsService _appSettingsService;

        public StatisticAppService(IRepository<Statistic> statisticRepository, AppSettingsService appSettingsService)
        {
            _statisticRepository = statisticRepository;
            _appSettingsService = appSettingsService;
        }

        public async Task<List<StatisticDto>> GetAllAsync()
        {
            var list = await _statisticRepository.TableNoTracking.ToListAsync();
            return list.MapTo<List<StatisticDto>>();
        }

        public async Task<StatisticDto> GetByIdAsync(long id)
        {
            var entity = await _statisticRepository.GetByIdAsync(id);
            return entity?.MapTo<StatisticDto>();
        }

        public async Task<long> InsertAsync(StatisticDto dto)
        {
            var entity = dto.MapTo<Statistic>();
            var inserted = await _statisticRepository.InsertAsync(entity, true);
            return inserted.Id;
        }

        public async Task<long> UpdateAsync(StatisticDto dto)
        {
            var entity = await _statisticRepository.GetByIdAsync(dto.Id);
            if (entity == null) return 0;

            entity = dto.MapTo<Statistic>();
            var updated = await _statisticRepository.UpdateAsync(entity, true);
            return updated.Id;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _statisticRepository.DeleteAsync(s => s.Id == id, true);
        }
    }
}
