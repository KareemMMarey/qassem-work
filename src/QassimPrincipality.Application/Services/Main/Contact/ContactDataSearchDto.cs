using Framework.Core.Data;
using PagedList.Core;
using QassimPrincipality.Application.Services.Main.ShareData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Main.Contact
{
    public class ContactDataSearchDto : PagingDto
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public string ContactTitle { get; set; }
        public string Description { get; set; }
        public int ContactTypeId { get; set; }
        public string IdentityNumber { get; set; }
        public bool? IsApproved { get; set; }
        public int? TotalItemsCount { get; set; }
        public new StaticPagedList<ContactFormDto> Items { get; set; }
    }
}
