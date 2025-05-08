using Framework.Core.AutoMapper;
using Framework.Core.SharedServices.Services;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using QassimPrincipality.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.NewShema
{
	public class PageFeedbackAppService
	{
		private readonly IRepository<PageFeedback> _feedbackRepository;
		private readonly AppSettingsService _appSettingsService;

		public PageFeedbackAppService(IRepository<PageFeedback> feedbackRepository, AppSettingsService appSettingsService)
		{
			_feedbackRepository = feedbackRepository;
			_appSettingsService = appSettingsService;
		}

		public async Task<List<PageFeedbackDto>> GetAllAsync()
		{
			var feedbackList = await _feedbackRepository.TableNoTracking.ToListAsync();
			return feedbackList.MapTo<List<PageFeedbackDto>>();
		}

		public async Task<PageFeedbackDto> GetByIdAsync(long id)
		{
			var entity = await _feedbackRepository.GetByIdAsync(id);
			return entity?.MapTo<PageFeedbackDto>();
		}

		public async Task<long> InsertAsync(PageFeedbackDto dto)
		{
			var entity = dto.MapTo<PageFeedback>();
			var inserted = await _feedbackRepository.InsertAsync(entity, true);
			return inserted.Id;
		}

		public async Task<long> UpdateAsync(PageFeedbackDto dto)
		{
			var entity = await _feedbackRepository.GetByIdAsync(dto.Id);
			if (entity == null) return 0;

			entity.TotalUsers = dto.TotalUsers;
			entity.PositiveResponses = dto.PositiveResponses;
			entity.NegativeResponses = dto.NegativeResponses;
			var updated = await _feedbackRepository.UpdateAsync(entity, true);
			return updated.Id;
		}

		public async Task<bool> DeleteAsync(long id)
		{
			return await _feedbackRepository.DeleteAsync(f => f.Id == id, true);
		}

		public async Task<PageFeedbackSearchDto> SearchAsync(PageFeedbackSearchDto searchDto)
		{
			var filters = new List<Expression<Func<PageFeedback, bool>>>();

			if (!string.IsNullOrWhiteSpace(searchDto.PageUrl))
				filters.Add(f => f.PageUrl.Contains(searchDto.PageUrl));

			Func<IQueryable<PageFeedback>, IOrderedQueryable<PageFeedback>> orderBy = q => q.OrderByDescending(f => f.CreatedOn);

			var result = _feedbackRepository.SearchAndSelectWithFilters(
				searchDto.PageNumber,
				searchDto.PageSize,
				orderBy,
				f => f.MapTo<PageFeedbackDto>(),
				filters
			);

			searchDto.Items = new StaticPagedList<PageFeedbackDto>(
				result,
				result.PageNumber,
				result.PageSize,
				result.TotalItemCount
			);

			searchDto.TotalItemsCount = searchDto.Items.TotalItemCount;
			return await Task.FromResult(searchDto);
		}

		public async Task<bool> SubmitFeedbackAsync(string pageUrl, bool isPositive)
		{
			var feedback = _feedbackRepository.Table.FirstOrDefault(f => f.PageUrl == pageUrl);

			if (feedback == null)
			{
				feedback = new PageFeedback
				{
					PageUrl = pageUrl,
					TotalUsers = 1,
					PositiveResponses = isPositive ? 1 : 0,
					NegativeResponses = isPositive ? 0 : 1
				};
				await _feedbackRepository.InsertAsync(feedback, true);
			}
			else
			{
				feedback.TotalUsers++;
				if (isPositive)
					feedback.PositiveResponses++;
				else
					feedback.NegativeResponses++;

				await _feedbackRepository.UpdateAsync(feedback, true);
			}

			return true;
		}
	}
}
