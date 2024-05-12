using Framework.Core.AutoMapper;
using Framework.Core.SharedServices.Services;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Main.Evaluation
{
    public class EvaluationAppService
    {
        private readonly IRepository<Domain.Entities.Services.Main.ServiceEvaluation> _evaluationRepository;
        private readonly AppSettingsService _appSettingsService;

        public EvaluationAppService(IRepository<Domain.Entities.Services.Main.ServiceEvaluation> evaluationRepository, AppSettingsService appSettingsService)
        {
            _evaluationRepository = evaluationRepository;
            _appSettingsService = appSettingsService;
        }

        public async Task<List<EvaluationDto>> GetAllEvaluations()
        {
            var evaluations = await _evaluationRepository.TableNoTracking.ToListAsync();
            return evaluations.MapTo<List<EvaluationDto>>();
        }
        public async Task<List<EvaluationDto>> GetAllEvaluationsByService(int serviceId)
        {
            var evaluations = await _evaluationRepository.TableNoTracking.Where(c=>c.SubCategoryId==serviceId).ToListAsync();
            return evaluations.MapTo<List<EvaluationDto>>();
        }
        public async Task<List<EvaluationDto>> CheckExist(int serviceId, string userNme)
        {
            var evaluations = await _evaluationRepository.TableNoTracking.Where(c => c.SubCategoryId == serviceId&&c.CreatedBy==userNme).ToListAsync();
            return evaluations.MapTo<List<EvaluationDto>>();
        }

        public async Task<List<EvaluationDto>> GetActiveContactRequest()
        {
            var eServiceCategory = await _evaluationRepository.TableNoTracking.ToListAsync();
            return eServiceCategory.MapTo<List<EvaluationDto>>();
        }

        public async Task<Domain.Entities.Services.Main.ServiceEvaluation> InsertAsync(EvaluationDto EvaluationDto)
        {
            var eServiceCategory = EvaluationDto.MapTo<Domain.Entities.Services.Main.ServiceEvaluation>();
            var saved = await _evaluationRepository.InsertAsync(eServiceCategory, true);
            return saved;
        }

        public async Task<EvaluationDto> GetById(Guid id)
        {
            try
            {
                var entity = await _evaluationRepository.GetByIdAsync(id);
                var EvaluationDto = entity.MapTo<EvaluationDto>();

                return await Task.FromResult(EvaluationDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateAsync(EvaluationDto evaluationDto)
        {
            if (evaluationDto.Id == Guid.Empty)
            {
                return Guid.Empty;
            }
            var oldData = await _evaluationRepository.TableNoTracking.FirstOrDefaultAsync(s => s.Id == evaluationDto.Id);
            if (oldData == null)
            {
                return Guid.Empty;
            }
            oldData = evaluationDto.MapTo<Domain.Entities.Services.Main.ServiceEvaluation>();
            var updatedItem = await _evaluationRepository.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var obj = await _evaluationRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    return await _evaluationRepository.DeleteAsync(m => m.Id == id, true);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<EvaluationSearchDto> SearchAsync(
           EvaluationSearchDto evaluationSearchDto
       )
        {
            var filters =
                new List<Expression<Func<Domain.Entities.Services.Main.ServiceEvaluation, bool>>>();


           



            Func<
                IQueryable<Domain.Entities.Services.Main.ServiceEvaluation>,
                IOrderedQueryable<Domain.Entities.Services.Main.ServiceEvaluation>
            > orderBy;
            orderBy = a => a.OrderByDescending(b => b.CreatedOn);
            Framework.Core.PagedList<EvaluationDto> result;

            result = _evaluationRepository.SearchAndSelectWithFilters(
                evaluationSearchDto.PageNumber,
                evaluationSearchDto.PageSize ?? _appSettingsService.DefaultPagerPageSize,
                orderBy,
                a => a.MapTo<EvaluationDto>(),
                filters
            );


            evaluationSearchDto.Items = new StaticPagedList<EvaluationDto>(
                result,
                result.PageNumber,
                result.PageSize,
                result.TotalItemCount
            );

            evaluationSearchDto.TotalItemsCount = evaluationSearchDto.Items.TotalItemCount;
            return await Task.FromResult(evaluationSearchDto);
        }
    }
}
