using Framework.Core.AutoMapper;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.Lookups.Main.CommonEService;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Domain.Entities.Services.NewSchema;
using QassimPrincipality.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Lookups.Main.SharedContactFormService
{
    public class SharedContactFormAppService
    {
        private readonly IRepository<SharedContactForm> repository;

        public SharedContactFormAppService(IRepository<SharedContactForm> repository)
        {
            this.repository = repository;
        }

        public async Task<SharedContactForm> InsertAsync(ContactUsModel EServiceCategoryDto)
        {
            var eServiceCategory = EServiceCategoryDto.MapTo<SharedContactForm>();
            var saved = await repository.InsertAsync(eServiceCategory, true);
            return saved;
        }
    }
}
