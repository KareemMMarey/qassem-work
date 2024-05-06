using Framework.Core.Data;
using PagedList.Core;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Main.OpenData
{
    public class OpenDataRequestSearchDto : PagingDto
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string IdentityNumber { get; set; }
        public string UserMobile { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RequesterTypeId { get; set; }
        public bool IsAllowed { get; set; }
        public bool? IsApproved { get; set; }
        public int? TotalItemsCount { get; set; }
        public new StaticPagedList<OpenDataDto> Items { get; set; }
    }
}
