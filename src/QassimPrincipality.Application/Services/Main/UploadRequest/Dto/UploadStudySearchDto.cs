using Framework.Core.Data;
using PagedList.Core;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestSearchDto : PagingDto
    {
        public string ReferralNumber { get; set; }
        public string RequestNumber { get; set; }
        public string RequestName { get; set; }
       
        public int? RequestTypeId { get; set; }
        public bool? IsApproved { get; set; }
        public bool? isPending { get; set; }
        public bool IsActive { get; set; }
        public string[] Tags { get; set; }
        public int? TotalItemsCount { get; set; }
        public new StaticPagedList<UploadRequestDto> Items { get; set; }
        public DateTime? RequestDateFrom { get; set; }
        public DateTime? RequestDateTo { get; set; }
    }

}