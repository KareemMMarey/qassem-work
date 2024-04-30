using QassimPrincipality.Domain.Entities.Lookups;
using Framework.Core.Data;
using Framework.Core.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using QassimPrincipality.Domain.Entities.Lookups.Main;

namespace QassimPrincipality.Domain.Entities.Services.Main
{
    public class UploadRequest : FullAuditedEntityBase<Guid>
    {
        public string referralNumber { get; set; }

        [NotMapped]
        public string RequestName => CultureHelper.IsArabic ? RequestNameAr : RequestNameEn;

        public string RequestNameAr { get; set; }
        public string RequestNameEn { get; set; }
        public int RequestSubClassificationId { get; set; }
        
        public string OriginalRequestId { get; set; }
        public int RequestTypeId { get; set; }
        public RequestType RequestType { get; set; }

        public int EServiceSubCategoryId { get; set; }
        public EServiceSubCategory EServiceSubCategory { get; set; }
        public Guid? OriginatorId { get; set; }
        public Guid? RequestOwnerId { get; set; } //from users table
        public string RequestOwnerNameAr { get; set; } //from users table

        //[NotMapped]
        //public string RequestOwnerName => CultureHelper.IsArabic ? RequestOwnerNameAr : RequestOwnerNameEn;

        public string ExecutiveSummary { get; set; } //from users table
        
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public string RequestNumber { get; set; }
        public string RequestSource { get; set; }
        public List<Attachment> Attachments { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}