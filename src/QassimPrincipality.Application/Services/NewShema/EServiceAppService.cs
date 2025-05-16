
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Application.Services.Lookups.Main.CommonEService;
using QassimPrincipality.Application.Dtos.Content;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;

namespace QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory
{
    public class EServiceAppService
    {
        private readonly IRepository<EService> _eServiceRepository;

        public EServiceAppService(IRepository<EService> eServiceRepository)
        {
            _eServiceRepository = eServiceRepository;
        }

        public async Task<List<GetEServiceListHome>> GetAll()
        {
            var eServiceCategory = await _eServiceRepository.TableNoTracking.
                Include(c => c.ServicesCategory).
                Include(c => c.EServiceDetails).
                Include(c => c.Ratings).
                ToListAsync();
            return eServiceCategory.MapTo<List<GetEServiceListHome>>();
        }
        /// <summary>
        /// Returns only three items from EService Table
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetEServiceListHome>> GetSimpleEServiceList()
        {
            var eServiceCategory = await _eServiceRepository.TableNoTracking.
                Include(c=>c.ServicesCategory).
                Include(c=>c.EServiceDetails).
                Include(c => c.Ratings).
                Where(s => s.IsActive).Take(3).ToListAsync();
            return eServiceCategory.MapTo<List<GetEServiceListHome>>();
        }

        public async Task<EService> InsertAsync(CommonEServiceDto EServiceCategoryDto)
        {
            var eServiceCategory = EServiceCategoryDto.MapTo<EService>();
            var saved = await _eServiceRepository.InsertAsync(eServiceCategory, true);
            return saved;
        }

        public async Task<CommonEServiceDto> GetById(int id)
        {
            try
            {
                var entity = await _eServiceRepository.GetByIdAsync(id);
                var EServiceCategoryDto = entity.MapTo<CommonEServiceDto>();

                return await Task.FromResult(EServiceCategoryDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<GetEServiceDetailsDto> GetServiceDetailsById(int id)
        {
            try
            {
                var entity = await _eServiceRepository.TableNoTracking.
                    Include(c => c.ServicesCategory).
                    Include(c => c.EServiceDetails).
                    Include(c => c.EServiceRequirements).
                    Include(c => c.FAQs).
                    Include(c => c.EServiceFlows).
                    Include(c => c.Ratings).
                    FirstOrDefaultAsync(c=>c.Id==id);
                var EServiceCategoryDto = entity.MapTo<GetEServiceDetailsDto>();

                return await Task.FromResult(EServiceCategoryDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<GetEServiceStepsDto> GetServiceStepsById(int id)
        {
            try
            {
                var entity = await _eServiceRepository.TableNoTracking.
                    Include(c => c.ServicesCategory).
                    Include(c => c.EServiceDetails).
                    Include(c => c.ServiceSteps).
                    FirstOrDefaultAsync(c => c.Id == id);
                var EServiceCategoryDto = entity.MapTo<GetEServiceStepsDto>();

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
            var oldData = await _eServiceRepository.TableNoTracking.FirstOrDefaultAsync(s => s.Id == EServiceCategoryDto.Id);
            if (oldData == null)
            {
                return 0;
            }
            oldData = EServiceCategoryDto.MapTo<EService>();
            var updatedItem = await _eServiceRepository.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var obj = await _eServiceRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    return await _eServiceRepository.DeleteAsync(m => m.Id == id, true);
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
                var obj = await _eServiceRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    obj.IsActive= status;
                    var updatedItem = await _eServiceRepository.UpdateAsync(obj, true);
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