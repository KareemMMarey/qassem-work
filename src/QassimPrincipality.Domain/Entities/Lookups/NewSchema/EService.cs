using Framework.Core.Data;
using Framework.Core.Notifications;
using QassimPrincipality.Domain.Entities.Services.NewSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
    public class EService : LookupEntityBase
    {
        public string DescriptionEn { get; set; }
		public string DescriptionAr { get; set; }
		public string ServiceCode { get; set; }
		public int EServiceFormId { get; set; }
        public int CategoryId { get; set; }
		public string IconUrl { get; set; }
        public string ServiceController { get; set; }
        public string ServiceActionMethos { get; set; }
        public virtual EServiceForm EServiceForm { get; set; }
        public virtual ServicesCategory ServicesCategory { get; set; }
        public virtual EServiceDetails EServiceDetails { get; set; }
        public virtual ICollection<ServiceRequest> Requests { get; set; }

        public virtual ICollection<ServiceTab> Tabs { get; set; }
        public virtual ICollection<ServiceFAQ> FAQs { get; set; }
        public virtual ICollection<ServiceRating> Ratings { get; set; }
        public virtual ICollection<AttachmentType> AttachmentTypes { get; set; }
		public virtual ICollection<EServiceRequirement> EServiceRequirements { get; set; }
		public virtual ICollection<EServiceFlow> EServiceFlows { get; set; }
        public virtual ICollection<ServiceStep> ServiceSteps { get; set; }
    }
}
