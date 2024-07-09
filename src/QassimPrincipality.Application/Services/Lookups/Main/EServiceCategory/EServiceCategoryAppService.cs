
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Application.Services.Lookups.Main.CommonEService;

namespace QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory
{
    public class EServiceCategoryAppService
    {
        private readonly IRepository<Domain.Entities.Lookups.Main.EServiceCategory> _eServiceCategoryRepository;

        public EServiceCategoryAppService(IRepository<Domain.Entities.Lookups.Main.EServiceCategory> eServiceCategoryRepository)
        {
            _eServiceCategoryRepository = eServiceCategoryRepository;
        }

        public async Task<List<EServiceCategoryDto>> GetAllEServiceCategories()
        {
            var eServiceCategory = await _eServiceCategoryRepository.TableNoTracking.ToListAsync();
            return eServiceCategory.MapTo<List<EServiceCategoryDto>>();
        }

        public async Task<List<EServiceCategoryDto>> GetActiveEServiceCategories()
        {
            var eServiceCategory = await _eServiceCategoryRepository.TableNoTracking.Where(s => s.IsActive).ToListAsync();
            return eServiceCategory.MapTo<List<EServiceCategoryDto>>();
        }

        public async Task<Domain.Entities.Lookups.Main.EServiceCategory> InsertAsync(CommonEServiceDto EServiceCategoryDto)
        {
            var eServiceCategory = EServiceCategoryDto.MapTo<Domain.Entities.Lookups.Main.EServiceCategory>();
            var saved = await _eServiceCategoryRepository.InsertAsync(eServiceCategory, true);
            return saved;
        }

        public async Task<CommonEServiceDto> GetById(int id)
        {
            try
            {
                var entity = await _eServiceCategoryRepository.GetByIdAsync(id);
                var EServiceCategoryDto = entity.MapTo<CommonEServiceDto>();

                return await Task.FromResult(EServiceCategoryDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(EServiceCategoryDto EServiceCategoryDto)
        {
            if (EServiceCategoryDto.Id == 0)
            {
                return 0;
            }
            var oldData = await _eServiceCategoryRepository.TableNoTracking.FirstOrDefaultAsync(s => s.Id == EServiceCategoryDto.Id);
            if (oldData == null)
            {
                return 0;
            }
            oldData = EServiceCategoryDto.MapTo<Domain.Entities.Lookups.Main.EServiceCategory>();
            var updatedItem = await _eServiceCategoryRepository.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var obj = await _eServiceCategoryRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    return await _eServiceCategoryRepository.DeleteAsync(m => m.Id == id, true);
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
        public async Task<int> ChangeActiveStatus(int id, bool status)
        {
            try
            {
                var obj = await _eServiceCategoryRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    obj.IsActive= status;
                    var updatedItem = await _eServiceCategoryRepository.UpdateAsync(obj, true);
                    return updatedItem.Id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}