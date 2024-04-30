using QassimPrincipality.Application.Services.Lookups.Main.RequestType.Dto;
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace QassimPrincipality.Application.Services.Lookups.Main.RequestType
{
    public class RequestTypeAppService
    {
        private readonly IRepository<Domain.Entities.Lookups.Main.RequestType> _requestTypeRepository;

        public RequestTypeAppService(IRepository<Domain.Entities.Lookups.Main.RequestType> requestTypeRepository)
        {
            _requestTypeRepository = requestTypeRepository;
        }

        public async Task<List<RequestTypeDto>> GetAllRequestTypes()
        {
            var requestType = await _requestTypeRepository.TableNoTracking.ToListAsync();
            return requestType.MapTo<List<RequestTypeDto>>();
        }

        public async Task<Domain.Entities.Lookups.Main.RequestType> InsertAsync(RequestTypeDto requestTypeDto)
        {
            var requestType = requestTypeDto.MapTo<Domain.Entities.Lookups.Main.RequestType>();
            var saved = await _requestTypeRepository.InsertAsync(requestType, true);
            return saved;
        }

        public async Task<RequestTypeDto> GetById(int id)
        {
            try
            {
                var entity = await _requestTypeRepository.GetByIdAsync(id);
                var requestTypeDto = entity.MapTo<RequestTypeDto>();

                return await Task.FromResult(requestTypeDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(RequestTypeDto requestTypeDto)
        {
            if (requestTypeDto.Id == 0)
            {
                return 0;
            }
            var oldData = await _requestTypeRepository.TableNoTracking.FirstOrDefaultAsync(s => s.Id == requestTypeDto.Id);
            if (oldData == null)
            {
                return 0;
            }
            oldData = requestTypeDto.MapTo<Domain.Entities.Lookups.Main.RequestType>();
            var updatedItem = await _requestTypeRepository.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var obj = await _requestTypeRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    return await _requestTypeRepository.DeleteAsync(m => m.Id == id, true);
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