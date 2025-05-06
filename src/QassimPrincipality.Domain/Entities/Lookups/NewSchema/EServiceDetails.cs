using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
    public class EServiceDetails : LookupEntityBase
    {

        public int ServiceId { get; set; }
        public string AudienceTypeAr { get; set; }
        public string AudienceTypeEn { get; set; }
        public string ExecutionTimeAr { get; set; }
        public string ExecutionTimeEn { get; set; }
        public string CostAr { get; set; }
        public string CostEn { get; set; }
        public virtual EService EService { get; set; }

    }
}
