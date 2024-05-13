using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Lookups.Main;

namespace QassimPrincipality.Domain.Entities.Services.Main
{
    public class ShareDataRequest : FullAuditedEntityBase<Guid>
    {
        public string ReferralNumber { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string IdentityNumber { get; set; }
        public string UserMobile { get; set; }
        public string Description { get; set; }
        public int EntityTypeId { get; set; }
        public string EntityName { get; set; }
        public string RejectReason { get; set; }
        public string PurposeOfRequest { get; set; }
        public EntityType EntityType { get; set; } // open data
        public bool? IsShareAgreementExist { get; set; }
        public bool? IsContainsPersonalData { get; set; }
        public bool? IsRequesterDataOfficePresenter { get; set; }
        public bool? IsLegalJustification { get; set; }
        public bool IsAllowed { get; set; }
        public bool? IsApproved { get; set; }
        public string LegalJustificationDescription { get; set; }
    }
}
