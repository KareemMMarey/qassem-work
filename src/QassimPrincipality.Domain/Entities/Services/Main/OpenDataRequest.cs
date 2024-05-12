
using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Lookups;
using QassimPrincipality.Domain.Entities.Lookups.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.Main
{
    public class OpenDataRequest : FullAuditedEntityBase<Guid>
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RequesterTypeId { get; set; }
        public bool IsAllowed { get; set; }
        public bool? IsApproved { get; set; }
        public string IdentityNumber { get; set; }
        public string RejectReason { get; set; }
        public RequesterType RequesterType { get; set; } // open data
        public List<Attachment> Attachments { get; set; }

    }
}
