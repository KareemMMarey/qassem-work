using Framework.Core.Data;
using PagedList.Core;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestSearchDto : PagingDto
    {
        public string ReferralNumber { get; set; }
        public string Researcher { get; set; }
        public string RequestNumber { get; set; }
        public string RequestName { get; set; }
        public int? requestClassificationId { get; set; }
        public int? RequestSubClassificationId { get; set; }
        public int? RequestTypeId { get; set; }
        public int? ConsultantId { get; set; }
        public int? LevelOfSecrecyId { get; set; }
        public Guid? RequestOwnerId { get; set; }
        public string RequestOwnerName { get; set; }
        public Guid? ResearcherId { get; set; }
        public bool EnglishVersionIsRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsManageStudies { get; set; }
        public bool IsRequestLibrary { get; set; }
        public string[] Tags { get; set; }
        public int? TotalItemsCount { get; set; }
        public new StaticPagedList<UploadRequestDto> Items { get; set; }
        public DateTime? RequestDateFrom { get; set; }
        public DateTime? RequestDateTo { get; set; }
    }
}