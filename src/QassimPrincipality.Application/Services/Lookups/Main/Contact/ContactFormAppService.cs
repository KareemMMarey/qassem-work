
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace QassimPrincipality.Application.Services.Lookups.Main.Contact
{
    public class ContactFormAppService
    {
        private readonly IRepository<Domain.Entities.Services.Main.ContactForm> _contactRepository;

        public ContactFormAppService(IRepository<Domain.Entities.Services.Main.ContactForm> eServiceCategoryRepository)
        {
            _contactRepository = eServiceCategoryRepository;
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

        public async Task<Domain.Entities.Services.Main.ContactForm> InsertAsync(ContactFormDto ContactFormDto)
        {
            var eServiceCategory = ContactFormDto.MapTo<Domain.Entities.Services.Main.ContactForm>();
            var saved = await _contactRepository.InsertAsync(eServiceCategory, true);
            return saved;
        }

        public async Task<ContactFormDto> GetById(int id)
        {
            try
            {
                var entity = await _contactRepository.GetByIdAsync(id);
                var ContactFormDto = entity.MapTo<ContactFormDto>();

                return await Task.FromResult(ContactFormDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateAsync(ContactFormDto ContactFormDto)
        {
            if (ContactFormDto.Id==Guid.Empty)
            {
                return Guid.Empty;
            }
            var oldData = await _contactRepository.TableNoTracking.FirstOrDefaultAsync(s => s.Id == ContactFormDto.Id);
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
                var obj = await _contactRepository.TableNoTracking.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}