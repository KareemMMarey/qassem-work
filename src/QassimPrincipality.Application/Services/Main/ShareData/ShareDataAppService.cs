
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Application.Services.Main.ShareData;

namespace QassimPrincipality.Application.Services.Main.ShareDataRequest
{
    public class ShareDataAppService
    {
        private readonly IRepository<Domain.Entities.Services.Main.ShareDataRequest> _repo;

        public ShareDataAppService(IRepository<Domain.Entities.Services.Main.ShareDataRequest> repo)
        {
            _repo = repo;
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

        public async Task<Domain.Entities.Services.Main.ShareDataRequest> InsertAsync(ShareDataDto ShareDataDto)
        {
            var shareDataRequest = ShareDataDto.MapTo<Domain.Entities.Services.Main.ShareDataRequest>();
            var saved = await _repo.InsertAsync(shareDataRequest, true);
            return saved;
        }

        public async Task<ShareDataDto> GetById(int id)
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
            if (ShareDataDto.Id==Guid.Empty)
            {
                return Guid.Empty;
            }
            var oldData = await _repo.TableNoTracking.FirstOrDefaultAsync(s => s.Id == ShareDataDto.Id);
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
    }
}