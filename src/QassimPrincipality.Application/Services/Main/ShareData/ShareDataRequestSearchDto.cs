using Framework.Core.Data;
using PagedList.Core;
using QassimPrincipality.Application.Services.Main.OpenData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Main.ShareData
{
    public class ShareDataRequestSearchDto : PagingDto
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string IdentityNumber { get; set; }
        public string UserMobile { get; set; }
        public string Description { get; set; }
        public int EntityTypeId { get; set; }
        public string EntityName { get; set; }
        public string PurposeOfRequest { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsShareAgreementExist { get; set; }
        public bool? IsContainsPersonalData { get; set; }
        public bool? IsRequesterDataOfficePresenter { get; set; }
        public bool? IsLegalJustification { get; set; }
        public bool IsAllowed { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsPending { get; set; }
        public int? TotalItemsCount { get; set; }
        public new StaticPagedList<ShareDataDto> Items { get; set; }
    }
}
