using QassimPrincipality.Application.Services.Main.ShareData;
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace QassimPrincipality.Application.Services.Main.Inquery
{
    public class InqueryAppService
    {
        private readonly IRepository<Domain.Entities.Services.Main.UploadRequest> _repo;

        public InqueryAppService(IRepository<Domain.Entities.Services.Main.UploadRequest> repo)
        {
            _repo = repo;
        }

        public async Task<List<InqueryDto>> GetAllContactRequest()
        {
            var shareDataRequest = await _repo.TableNoTracking.ToListAsync();
            return shareDataRequest.MapTo<List<InqueryDto>>();
        }

        public async Task<List<InqueryDto>> GetActiveContactRequest()
        {
            var shareDataRequest = await _repo.TableNoTracking.ToListAsync();
            return shareDataRequest.MapTo<List<InqueryDto>>();
        }

        public async Task<Domain.Entities.Services.Main.UploadRequest> InsertAsync(InqueryDto InqueryDto)
        {
            var shareDataRequest = InqueryDto.MapTo<Domain.Entities.Services.Main.UploadRequest>();
            var saved = await _repo.InsertAsync(shareDataRequest, true);
            return saved;
        }

        public async Task<InqueryDto> GetById(int id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                var InqueryDto = entity.MapTo<InqueryDto>();

                return await Task.FromResult(InqueryDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //public async Task<Guid> UpdateAsync(InqueryDto InqueryDto)
        //{
        //    if (InqueryDto.Id == Guid.Empty)
        //    {
        //        return Guid.Empty;
        //    }
        //    var oldData = await _repo.TableNoTracking.FirstOrDefaultAsync(s => s.Id == InqueryDto.Id);
        //    if (oldData == null)
        //    {
        //        return Guid.Empty;
        //    }
        //    oldData = InqueryDto.MapTo<Domain.Entities.Services.Main.UploadRequest>();
        //    var updatedItem = await _repo.UpdateAsync(oldData, true);
        //    return updatedItem.Id;
        //}

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
