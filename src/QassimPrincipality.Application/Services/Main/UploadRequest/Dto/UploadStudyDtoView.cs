using QassimPrincipality.Application.Dtos;
using Framework.Core.Globalization;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestDtoView : UploadRequestDto
    {
        public string requestClassificationName => CultureHelper.IsArabic ? requestClassificationNameAr : requestClassificationNameEn;
        public string requestClassificationNameAr { get; set; }
        public string requestClassificationNameEn { get; set; }
        public string RequestSubClassificationName => CultureHelper.IsArabic ? RequestSubClassificationNameAr : RequestSubClassificationNameEn;
        public string RequestSubClassificationNameAr { get; set; }
        public string RequestSubClassificationNameEn { get; set; }
        public string RequestTypeName => CultureHelper.IsArabic ? RequestTypeNameAr : RequestTypeNameEn;
        public string RequestTypeNameAr { get; set; }
        public string RequestTypeNameEn { get; set; }
        
        public string LevelOfSecrecyName => CultureHelper.IsArabic ? LevelOfSecrecyNameAr : LevelOfSecrecyNameEn;
        public string LevelOfSecrecyNameAr { get; set; }
        public string LevelOfSecrecyNameEn { get; set; }
        public string ConsultantName => CultureHelper.IsArabic ? ConsultantNameAr : ConsultantNameEn;
        public string ConsultantNameAr { get; set; }
        public string ConsultantNameEn { get; set; }
        public Guid? RequestOwnerId { get; set; }
        public string RequestOwnerNameAr { get; set; }
        public string RequestOwnerNameEn { get; set; }
        public string RequestOwnerName => CultureHelper.IsArabic ? RequestOwnerNameAr : RequestOwnerNameEn;
        public string RequestSource { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}