using System.Linq.Expressions;
using Framework.Core.AutoMapper;
using Framework.Core.Extensions;
using Framework.Core.SharedServices.Services;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using QassimPrincipality.Domain.Interfaces;

namespace QassimPrincipality.Application.Services.Main.Contact
{
    public class ContactFormAppService
    {
        private readonly IRepository<Domain.Entities.Services.Main.ContactForm> _contactRepository;
        private readonly AppSettingsService _appSettingsService;

        public ContactFormAppService(
            IRepository<Domain.Entities.Services.Main.ContactForm> eServiceCategoryRepository,
            AppSettingsService appSettingsService
        )
        {
            _contactRepository = eServiceCategoryRepository;
            _appSettingsService = appSettingsService;
        }

        public async Task<List<ContactFormDto>> GetAllContactRequest()
        {
            var eServiceCategory = await _contactRepository.TableNoTracking.ToListAsync();
            return eServiceCategory.MapTo<List<ContactFormDto>>();
        }

        public async Task<List<ContactFormDto>> GetActiveContactRequest()
        {
            var eServiceCategory = await _contactRepository.TableNoTracking.ToListAsync();
            return eServiceCategory.MapTo<List<ContactFormDto>>();
        }

        public async Task<Domain.Entities.Services.Main.ContactForm> InsertAsync(
            ContactFormDto ContactFormDto
        )
        {
            var eServiceCategory =
                ContactFormDto.MapTo<Domain.Entities.Services.Main.ContactForm>();
            var saved = await _contactRepository.InsertAsync(eServiceCategory, true);
            return saved;
        }

        public async Task<ContactFormDto> GetById(Guid id)
        {
            try
            {
                var entity = await _contactRepository
                    .TableNoTracking.Include(c => c.ContactType)
                    .FirstOrDefaultAsync(c => c.Id == id);
                var ContactFormDto = entity.MapTo<ContactFormDto>();
                ContactFormDto.ContactTypeName = entity.ContactType.NameAr;
                return await Task.FromResult(ContactFormDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateAsync(ContactFormDto ContactFormDto)
        {
            if (ContactFormDto.Id == Guid.Empty)
            {
                return Guid.Empty;
            }
            var oldData = await _contactRepository.TableNoTracking.FirstOrDefaultAsync(s =>
                s.Id == ContactFormDto.Id
            );
            if (oldData == null)
            {
                return Guid.Empty;
            }
            oldData = ContactFormDto.MapTo<Domain.Entities.Services.Main.ContactForm>();
            var updatedItem = await _contactRepository.UpdateAsync(oldData, true);
            return updatedItem.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var obj = await _contactRepository.TableNoTracking.FirstOrDefaultAsync(m =>
                    m.Id == id
                );
                if (obj != null)
                {
                    return await _contactRepository.DeleteAsync(m => m.Id == id, true);
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

        public async Task<ContactDataSearchDto> SearchAsync(ContactDataSearchDto contactSearchDto)
        {
            var filters =
                new List<Expression<Func<Domain.Entities.Services.Main.ContactForm, bool>>>();

            if (contactSearchDto.IsApproved != null)
                filters.Add(a => a.IsApproved == contactSearchDto.IsApproved);

            if (!contactSearchDto.CreatedBy.IsNullOrWhiteSpace())
                filters.Add(a => a.CreatedBy == contactSearchDto.CreatedBy);

            Func<
                IQueryable<Domain.Entities.Services.Main.ContactForm>,
                IOrderedQueryable<Domain.Entities.Services.Main.ContactForm>
            > orderBy;
            orderBy = a => a.OrderByDescending(b => b.CreatedOn);
            Framework.Core.PagedList<ContactFormDto> result;

            result = _contactRepository.SearchAndSelectWithFilters(
                contactSearchDto.PageNumber,
                contactSearchDto.PageSize ?? _appSettingsService.DefaultPagerPageSize,
                orderBy,
                a => a.MapTo<ContactFormDto>(),
                filters
            );

            contactSearchDto.Items = new StaticPagedList<ContactFormDto>(
                result,
                result.PageNumber,
                result.PageSize,
                result.TotalItemCount
            );

            contactSearchDto.TotalItemsCount = contactSearchDto.Items.TotalItemCount;
            return await Task.FromResult(contactSearchDto);
        }
    }
}
