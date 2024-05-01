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

        public int RequestTypeId { get; set; }
        public RequestType RequestType { get; set; }

        public List<Attachment> Attachments { get; set; }
        public DateTime? RequestDate { get; set; }
        public string OracleRequestNumber { get; set; }
        public bool IsApproved { get; set; }
    }
}