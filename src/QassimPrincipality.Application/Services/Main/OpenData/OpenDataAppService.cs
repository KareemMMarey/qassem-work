
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Framework.Core.SharedServices.Services;
using PagedList.Core;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using System.Linq.Expressions;

namespace QassimPrincipality.Application.Services.Main.OpenData
{
    public class OpenDataAppService
    {
        private readonly IRepository<Domain.Entities.Services.Main.OpenDataRequest> _openDataRepository;
        private readonly AppSettingsService _appSettingsService;
        public OpenDataAppService(IRepository<Domain.Entities.Services.Main.OpenDataRequest> openDataRepository, AppSettingsService appSettingsService)
        {
            _openDataRepository = openDataRepository;
            _appSettingsService = appSettingsService;
        }

        public async Task<List<OpenDataDto>> GetAllContactRequest()
        {
            var eServiceCategory = await _openDataRepository.TableNoTracking.ToListAsync();
            return eServiceCategory.MapTo<List<OpenDataDto>>();
        }

        public async Task<List<OpenDataDto>> GetActiveContactRequest()
        {
            var eServiceCategory = await _openDataRepository.TableNoTracking.ToListAsync();
            return eServiceCategory.MapTo<List<OpenDataDto>>();
        }

        public async Task<Domain.Entities.Services.Main.OpenDataRequest> InsertAsync(OpenDataDto openDataDto)
        {
            var openDataRequest = openDataDto.MapTo<Domain.Entities.Services.Main.OpenDataRequest>();
            var saved = await _openDataRepository.InsertAsync(openDataRequest, true);
            return saved;
        }

        public async Task<OpenDataDto> GetById(Guid id)
        {
            try
            {
                var entity = await _openDataRepository.GetByIdAsync(id);
                var OpenDataDto = entity.MapTo<OpenDataDto>();

                return await Task.FromResult(OpenDataDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateAsync(OpenDataDto OpenDataDto)
        {
            if (OpenDataDto.Id==Guid.Empty)
            {
                return Guid.Empty;
            }
            var oldData = await _openDataRepository.TableNoTracking.FirstOrDefaultAsync(s => s.Id == OpenDataDto.Id);
            if (oldData == null)
            {
                return Guid.Empty;
            }
            oldData = OpenDataDto.MapTo<Domain.Entities.Services.Main.OpenDataRequest>();
            var updatedItem = await _openDataRepository.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var obj = await _openDataRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    return await _openDataRepository.DeleteAsync(m => m.Id == id, true);
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
        public async Task<OpenDataRequestSearchDto> SearchAsync(
            OpenDataRequestSearchDto openDataDto
        )
        {
            var filters =
                new List<Expression<Func<Domain.Entities.Services.Main.OpenDataRequest, bool>>>();


            if (openDataDto.IsApproved != null)
                filters.Add(a => a.IsApproved == openDataDto.IsApproved);

           

            Func<
                IQueryable<Domain.Entities.Services.Main.OpenDataRequest>,
                IOrderedQueryable<Domain.Entities.Services.Main.OpenDataRequest>
            > orderBy;
            orderBy = a => a.OrderByDescending(b => b.CreatedOn);

            Framework.Core.PagedList<OpenDataDto> result;

            result = _openDataRepository.SearchAndSelectWithFilters(
                openDataDto.PageNumber,
                openDataDto.PageSize ?? _appSettingsService.DefaultPagerPageSize,
                orderBy,
                a => a.MapTo<OpenDataDto>(),
                filters
            );


            openDataDto.Items = new StaticPagedList<OpenDataDto>(
                result,
                result.PageNumber,
                result.PageSize,
                result.TotalItemCount
            );

            openDataDto.TotalItemsCount = openDataDto.Items.TotalItemCount;
            return await Task.FromResult(openDataDto);
        }
    }
}