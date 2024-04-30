
using Framework.Core.Data;
using Framework.Core.Globalization;
using QassimPrincipality.Domain.Entities.Services.Main;
using System.ComponentModel.DataAnnotations.Schema;

namespace QassimPrincipality.Domain.Entities.Lookups
{
    public partial class Attachment : FullAuditedEntityBase<Guid>
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public bool IsArabic { get; set; } = true;
        public string ContentType { get; set; }

        [NotMapped]
        public string Title => CultureHelper.IsArabic ? TitleAr : TitleEn;

        public string TitleAr { get; set; }
        public string TitleEn { get; set; }

        [NotMapped]
        public string Description => CultureHelper.IsArabic ? DescriptionAr : DescriptionEn;

        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        //public byte[] Thumbnail { get; set; }
        //public byte[] FileContent { get; set; }

        //new
        public bool? IsOpenSourceArabic { get; set; } = false;

        public bool? IsOpenSourceEnglish { get; set; } = false;
        public bool? IsCloseSourceArabic { get; set; } = false;
        public bool? IsCloseSourceEnglish { get; set; } = false;
        public bool? IsData { get; set; } = false;
        public bool? IsSupporting { get; set; } = false;
        public bool? IsSanitizedDocument { get; set; } = false;

        public Guid? UploadRequestId { get; set; }
        public UploadRequest UploadRequest { get; set; }
        public AttachmentContent AttachmentContent { get; set; }
    }
}