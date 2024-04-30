using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Lookups.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.Main
{
    public class ContactForm : FullAuditedEntityBase<Guid>
    {
        public string UserEmail { get; set; }
        public string Description { get; set; }
        public int ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
    }
}
