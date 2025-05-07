using Framework.Core.AutoMapper;
using Framework.Core.SharedServices.Services;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using QassimPrincipality.Application.Dtos.Content;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content;
using QassimPrincipality.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.NewShema.Content
{
    public class NewsAppService
    {
        private readonly IRepository<News> _newsRepository;
        private readonly AppSettingsService _appSettingsService;

        public NewsAppService(IRepository<News> newsRepository, AppSettingsService appSettingsService)
        {
            _newsRepository = newsRepository;
            _appSettingsService = appSettingsService;
        }

        public async Task<List<NewsDto>> GetAllAsync()
        {
            var newsList = await _newsRepository.TableNoTracking.ToListAsync();
            return newsList.MapTo<List<NewsDto>>();
        }
		public async Task<List<NewsDto>> GetNewsForHomeAsync()
		{
			var newsList = await _newsRepository.TableNoTracking.Take(3).OrderByDescending(c=>c.PublishDate).ToListAsync();
			return newsList.MapTo<List<NewsDto>>();
		}

		public async Task<NewsDto> GetByIdAsync(long id)
        {
            var entity = await _newsRepository.GetByIdAsync(id);
            return entity?.MapTo<NewsDto>();
        }

        public async Task<long> InsertAsync(NewsDto dto)
        {
            var entity = dto.MapTo<News>();
            var inserted = await _newsRepository.InsertAsync(entity, true);
            return inserted.Id;
        }

        public async Task<long> UpdateAsync(NewsDto dto)
        {
            var entity = await _newsRepository.GetByIdAsync(dto.Id);
            if (entity == null) return 0;

            entity = dto.MapTo<News>();
            var updated = await _newsRepository.UpdateAsync(entity, true);
            return updated.Id;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _newsRepository.DeleteAsync(n => n.Id == id, true);
        }
        public async Task<NewsSearchDto> SearchAsync(NewsSearchDto searchDto)
        {
            var filters = new List<Expression<Func<News, bool>>>();

            if (!string.IsNullOrWhiteSpace(searchDto.Title))
                filters.Add(n => n.NameAr.Contains(searchDto.Title) || n.NameEn.Contains(searchDto.Title));

            if (searchDto.PublishDateFrom.HasValue)
                filters.Add(n => n.PublishDate >= searchDto.PublishDateFrom.Value);

            if (searchDto.PublishDateTo.HasValue)
                filters.Add(n => n.PublishDate <= searchDto.PublishDateTo.Value);

            if (searchDto.IsPublished.HasValue)
                filters.Add(n => n.IsPublished == searchDto.IsPublished.Value);

            Func<IQueryable<News>, IOrderedQueryable<News>> orderBy = q => q.OrderByDescending(n => n.PublishDate);

            var result = _newsRepository.SearchAndSelectWithFilters(
                searchDto.PageNumber,
                searchDto.PageSize ?? _appSettingsService.DefaultPagerPageSize,
                orderBy,
                n => n.MapTo<NewsDto>(),
                filters
            );

            searchDto.Items = new StaticPagedList<NewsDto>(
                result,
                result.PageNumber,
                result.PageSize,
                result.TotalItemCount
            );

            searchDto.TotalItemsCount = searchDto.Items.TotalItemCount;
            return await Task.FromResult(searchDto);
        }

    }

}
