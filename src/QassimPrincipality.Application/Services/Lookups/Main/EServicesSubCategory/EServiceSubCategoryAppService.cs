
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Application.Services.Lookups.Main.EServicesSubCategory;
using Framework.Core.Extensions;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.Lookups.Main.CommonEService;

namespace QassimPrincipality.Application.Services.Lookups.Main.EServiceSubCategory
{
    public class EServiceSubCategoryAppService
    {
        private readonly IRepository<Domain.Entities.Lookups.Main.EServiceSubCategory> _eServiceSubCategoryRepository;

        public EServiceSubCategoryAppService(IRepository<Domain.Entities.Lookups.Main.EServiceSubCategory> eServiceCategoryRepository)
        {
            _eServiceSubCategoryRepository = eServiceCategoryRepository;
        }

        public async Task<List<EServiceSubCategoryDto>> GetAllEServiceSubCategories(int? category)
        {
            var eServiceCategory = await _eServiceSubCategoryRepository.TableNoTracking.Where(s => s.CategoryId==category).ToListAsync();
            return eServiceCategory.MapTo<List<EServiceSubCategoryDto>>();
        }

        public async Task<List<EServiceSubCategoryDto>> GetActiveEServiceSubCategories(int? category)
        {
            var eServiceCategory = await _eServiceSubCategoryRepository.TableNoTracking.Where(s => s.IsActive&& s.CategoryId == category).ToListAsync();
            return eServiceCategory.MapTo<List<EServiceSubCategoryDto>>();
        }

        public async Task<Domain.Entities.Lookups.Main.EServiceSubCategory> InsertAsync(CommonEServiceDto EServiceSubCategoryDto)
        {
            var eServiceCategory = EServiceSubCategoryDto.MapTo<Domain.Entities.Lookups.Main.EServiceSubCategory>();
            var saved = await _eServiceSubCategoryRepository.InsertAsync(eServiceCategory, true);
            return saved;
        }

        public async Task<CommonEServiceDto> GetById(int id)
        {
            try
            {
                var entity = await _eServiceSubCategoryRepository.GetByIdAsync(id);
                var EServiceSubCategoryDto = entity.MapTo<CommonEServiceDto>();

                return await Task.FromResult(EServiceSubCategoryDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<List<CommonEServiceDto>> GetByCategoryId(int id)
        {
            try
            {
                var entity = await _eServiceSubCategoryRepository.TableNoTracking.Where(s => s.CategoryId==id).FirstOrDefaultAsync();
                var EServiceSubCategoryDto = entity.MapTo<List<CommonEServiceDto>>();

                return await Task.FromResult(EServiceSubCategoryDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<EServiceSubCategoryDto> GetActiveByCategoryId(int id)
        {
            try
            {
                var entity = await _eServiceSubCategoryRepository.TableNoTracking.Where(s => s.CategoryId == id&&s.IsActive).FirstOrDefaultAsync();
                var EServiceSubCategoryDto = entity.MapTo<EServiceSubCategoryDto>();

                return await Task.FromResult(EServiceSubCategoryDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(EServiceSubCategoryDto EServiceSubCategoryDto)
        {
            if (EServiceSubCategoryDto.Id == 0)
            {
                return 0;
            }
            var oldData = await _eServiceSubCategoryRepository.TableNoTracking.FirstOrDefaultAsync(s => s.Id == EServiceSubCategoryDto.Id);
            if (oldData == null)
            {
                return 0;
            }
            oldData = EServiceSubCategoryDto.MapTo<Domain.Entities.Lookups.Main.EServiceSubCategory>();
            var updatedItem = await _eServiceSubCategoryRepository.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var obj = await _eServiceSubCategoryRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    return await _eServiceSubCategoryRepository.DeleteAsync(m => m.Id == id, true);
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
                var obj = await _eServiceSubCategoryRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    obj.IsActive = status;
                    var updatedItem = await _eServiceSubCategoryRepository.UpdateAsync(obj, true);
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