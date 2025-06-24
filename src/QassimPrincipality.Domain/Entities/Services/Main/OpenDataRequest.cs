using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Lookups;
using QassimPrincipality.Domain.Entities.Lookups.Main;

namespace QassimPrincipality.Domain.Entities.Services.Main
{
    public class OpenDataRequest : FullAuditedEntityBase<Guid>
    {
        public string ReferralNumber { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RequesterTypeId { get; set; }
        public bool IsAllowed { get; set; }
        public bool? IsApproved { get; set; }
        public string IdentityNumber { get; set; }
        public string RejectReason { get; set; }
        public RequesterType RequesterType { get; set; } // open data
    }
}
