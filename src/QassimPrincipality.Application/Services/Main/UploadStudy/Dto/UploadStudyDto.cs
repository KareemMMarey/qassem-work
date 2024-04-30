using QassimPrincipality.Application.Dtos;
using Framework.Core.Globalization;
using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestDto
    {
        public Guid? Id { get; set; }
        public int LevelOfSecrecyId { get; set; }
        public int LevelOfSecrecyCode { get; set; }
        public string referralNumber { get; set; }
        public string OriginalRequestId { get; set; }
        public Guid? OriginatorId { get; set; }
        public string RequestName => CultureHelper.IsArabic ? RequestNameAr : RequestNameEn;
        public string RequestNameAr { get; set; }
        public string RequestNameEn { get; set; }
        public string RequestOwnerName => CultureHelper.IsArabic ? RequestOwnerNameAr : RequestOwnerNameEn;
        public string RequestOwnerNameAr { get; set; }
        public string RequestOwnerNameEn { get; set; }
        [StringLength(15000)]
        public string ExecutiveSummary { get; set; }
        public string[] Tags { get; set; }
        public Guid[] RelatedRequestId { get; set; }
        public Guid[] RequestTeam { get; set; }
        public bool? ApplicantIsRequestOwner { get; set; }
        public string RequestOwnerId { get; set; }
        public string RequestOwnerUserName { get; set; }
        public bool? EnglishVersionIsRequired { get; set; }
        public Guid? OpenSourceArPPTId { get; set; }
        public Guid? OpenSourceArDOCXId { get; set; }
        public Guid? CloseSourceArId { get; set; }
        public Guid? DataFileArId { get; set; }
        public Guid? OpenSourceEnPPTId { get; set; }
        public Guid? OpenSourceEnDOCXId { get; set; }
        public Guid? CloseSourceEnId { get; set; }
        public Guid? DataFileEnId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsApproved { get; set; }
        public bool CanAccessFiles { get; set; }
        public List<UploadRequestDto> RelatedStudies { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public string RequestNumber { get; set; }
        public string RequestOwneruserName { get; set; }
        public bool CurrentUserHasAccess { get; set; } = false;
        public bool CurrentUserHasRequestAccessPending { get; set; } = false;
        public DateTime? RequestDate { get; set; }
    }
}