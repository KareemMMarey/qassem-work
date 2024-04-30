using QassimPrincipality.Application.Services.Lookups.Main.RequestClassification.Dto;
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace QassimPrincipality.Application.Services.Lookups.Main.RequestClassification
{
    public class ClassificationAppService
    {
        private readonly IRepository<Domain.Entities.Lookups.Main.Classification> _requestClassificationRepository;

        public ClassificationAppService(IRepository<Domain.Entities.Lookups.Main.Classification> requestClassificationRepository)
        {
            _requestClassificationRepository = requestClassificationRepository;
        }

        public async Task<List<RequestClassificationDto>> GetAllRequestClassifications()
        {
            var requestClassification = await _requestClassificationRepository.TableNoTracking.ToListAsync();
            return requestClassification.MapTo<List<RequestClassificationDto>>();
        }

        public async Task<List<RequestClassificationDto>> GetActiveRequestClassifications()
        {
            var requestClassification = await _requestClassificationRepository.TableNoTracking.Where(s => s.IsActive).ToListAsync();
            return requestClassification.MapTo<List<RequestClassificationDto>>();
        }

        public async Task<Domain.Entities.Lookups.Main.Classification> InsertAsync(RequestClassificationDto RequestClassificationDto)
        {
            var requestClassification = RequestClassificationDto.MapTo<Domain.Entities.Lookups.Main.Classification>();
            var saved = await _requestClassificationRepository.InsertAsync(requestClassification, true);
            return saved;
        }

        public async Task<RequestClassificationDto> GetById(int id)
        {
            try
            {
                var entity = await _requestClassificationRepository.GetByIdAsync(id);
                var RequestClassificationDto = entity.MapTo<RequestClassificationDto>();

                return await Task.FromResult(RequestClassificationDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(RequestClassificationDto RequestClassificationDto)
        {
            if (RequestClassificationDto.Id == 0)
            {
                return 0;
            }
            var oldData = await _requestClassificationRepository.TableNoTracking.FirstOrDefaultAsync(s => s.Id == RequestClassificationDto.Id);
            if (oldData == null)
            {
                return 0;
            }
            oldData = RequestClassificationDto.MapTo<Domain.Entities.Lookups.Main.Classification>();
            var updatedItem = await _requestClassificationRepository.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var obj = await _requestClassificationRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    return await _requestClassificationRepository.DeleteAsync(m => m.Id == id, true);
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