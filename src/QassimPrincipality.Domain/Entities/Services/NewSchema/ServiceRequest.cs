using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.NewSchema
{
    public class ServiceRequest : FullAuditedEntityBase<Guid>
    {
        public int ServiceId { get; set; }
        public string UserId { get; set; }
        public string RequestNumber { get; set; } = string.Empty;
        public ServiceRequestStatus Status { get; set; }
        public ServiceRequesterRelation ServiceRequesterRelation { get; set; }

        public virtual EService EService { get; set; }
        public virtual RequestBasicData BasicData { get; set; }
        public virtual RequestAdditionalData AdditionalData { get; set; }
        public virtual ICollection<RequestAttachment>? Attachments { get; set; }
        public virtual ICollection<RequestAction> Actions { get; set; }
    }
}
