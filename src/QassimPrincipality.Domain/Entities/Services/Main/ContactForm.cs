using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Lookups.Main;

namespace QassimPrincipality.Domain.Entities.Services.Main
{
    public class ContactForm : FullAuditedEntityBase<Guid>
    {
        public string ReferralNumber { get; set; }

        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string IdentityNumber { get; set; }
        public string UserMobile { get; set; }
        public string ContactTitle { get; set; }
        public string Description { get; set; }
        public int ContactTypeId { get; set; }
        public bool? IsApproved { get; set; }
        public ContactType ContactType { get; set; }
    }
}
