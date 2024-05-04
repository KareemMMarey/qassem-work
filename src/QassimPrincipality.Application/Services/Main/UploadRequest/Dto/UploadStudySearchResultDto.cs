using Framework.Core.Globalization;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestSearchResultDto
    {
        public Guid? Id { get; set; }
        public string referralNumber { get; set; }
        public string RequestName => CultureHelper.IsArabic ? RequestNameAr : RequestNameEn;
        public string RequestNameAr { get; set; }
        public string RequestNameEn { get; set; }
        public int RequestSubClassificationId { get; set; }
        public string RequestSubClassificationName => CultureHelper.IsArabic ? RequestSubClassificationNameAr : RequestSubClassificationNameEn;
        public string RequestSubClassificationNameAr { get; set; }
        public string RequestSubClassificationNameEn { get; set; }
        public int RequestTypeId { get; set; }
        public string RequestType => CultureHelper.IsArabic ? RequestTypeAr : RequestTypeEn;
        public string RequestTypeAr { get; set; }
        public string RequestTypeEn { get; set; }
        public int? ConsultantId { get; set; }
        public string Consultant => CultureHelper.IsArabic ? ConsultantNameAr : ConsultantNameEn;
        public string ConsultantNameAr { get; set; }
        public string ConsultantNameEn { get; set; }
       
        public int LevelOfSecrecyId { get; set; }
        public string LevelOfSecrecy => CultureHelper.IsArabic ? LevelOfSecrecyAr : LevelOfSecrecyEn;
        public string LevelOfSecrecyAr { get; set; }
        public string LevelOfSecrecyEn { get; set; }
        public Guid RequestOwnerId { get; set; }
        public string RequestOwnerNameAr { get; set; }
        public string RequestOwnerNameEn { get; set; }
        public string RequestOwnerName => CultureHelper.IsArabic ? RequestOwnerNameAr : RequestOwnerNameEn;
        public string ExecutiveSummary { get; set; }
    }
}