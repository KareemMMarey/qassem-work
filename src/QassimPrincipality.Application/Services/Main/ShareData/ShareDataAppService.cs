using System.Linq.Expressions;
using Framework.Core.AutoMapper;
using Framework.Core.Extensions;
using Framework.Core.SharedServices.Services;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using QassimPrincipality.Application.Services.Main.OpenData;
using QassimPrincipality.Application.Services.Main.ShareData;
using QassimPrincipality.Domain.Interfaces;

namespace QassimPrincipality.Application.Services.Main.ShareDataRequest
{
    public class ShareDataAppService
    {
        private readonly IRepository<Domain.Entities.Services.Main.ShareDataRequest> _repo;
        private readonly AppSettingsService _appSettingsService;

        public ShareDataAppService(
            IRepository<Domain.Entities.Services.Main.ShareDataRequest> repo,
            AppSettingsService appSettingsService
        )
        {
            _repo = repo;
            _appSettingsService = appSettingsService;
        }

        public async Task<List<ShareDataDto>> GetAllContactRequest()
        {
            var shareDataRequest = await _repo.TableNoTracking.ToListAsync();
            return shareDataRequest.MapTo<List<ShareDataDto>>();
        }

        public async Task<List<ShareDataDto>> GetActiveContactRequest()
        {
            var shareDataRequest = await _repo.TableNoTracking.ToListAsync();
            return shareDataRequest.MapTo<List<ShareDataDto>>();
        }

        public async Task<Domain.Entities.Services.Main.ShareDataRequest> InsertAsync(
            ShareDataDto ShareDataDto
        )
        {
            var shareDataRequest =
                ShareDataDto.MapTo<Domain.Entities.Services.Main.ShareDataRequest>();
            var saved = await _repo.InsertAsync(shareDataRequest, true);
            return saved;
        }

        public async Task<ShareDataDto> GetById(Guid id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                var ShareDataDto = entity.MapTo<ShareDataDto>();

                return await Task.FromResult(ShareDataDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateAsync(ShareDataDto ShareDataDto)
        {
            if (ShareDataDto.Id == Guid.Empty)
            {
                return Guid.Empty;
            }
            var oldData = await _repo.TableNoTracking.FirstOrDefaultAsync(s =>
                s.Id == ShareDataDto.Id
            );
            if (oldData == null)
            {
                return Guid.Empty;
            }
            oldData = ShareDataDto.MapTo<Domain.Entities.Services.Main.ShareDataRequest>();
            var updatedItem = await _repo.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var obj = await _repo.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    return await _repo.DeleteAsync(m => m.Id == id, true);
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

        public async Task<ShareDataRequestSearchDto> SearchAsync(
            ShareDataRequestSearchDto shareDataDto
        )
        {
            var filters =
                new List<Expression<Func<Domain.Entities.Services.Main.ShareDataRequest, bool>>>();

            if (shareDataDto.IsApproved != null)
                filters.Add(a => a.IsApproved == shareDataDto.IsApproved);

            if (!shareDataDto.CreatedBy.IsNullOrWhiteSpace())
                filters.Add(a => a.CreatedBy == shareDataDto.CreatedBy);

            Func<
                IQueryable<Domain.Entities.Services.Main.ShareDataRequest>,
                IOrderedQueryable<Domain.Entities.Services.Main.ShareDataRequest>
            > orderBy;
            orderBy = a => a.OrderByDescending(b => b.CreatedOn);
            Framework.Core.PagedList<ShareDataDto> result;

            result = _repo.SearchAndSelectWithFilters(
                shareDataDto.PageNumber,
                shareDataDto.PageSize ?? _appSettingsService.DefaultPagerPageSize,
                orderBy,
                a => a.MapTo<ShareDataDto>(),
                filters
            );

            shareDataDto.Items = new StaticPagedList<ShareDataDto>(
                result,
                result.PageNumber,
                result.PageSize,
                result.TotalItemCount
            );

            shareDataDto.TotalItemsCount = shareDataDto.Items.TotalItemCount;
            return await Task.FromResult(shareDataDto);
        }
    }
}
