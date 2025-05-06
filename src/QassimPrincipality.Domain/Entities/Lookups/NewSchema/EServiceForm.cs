using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
    public class EServiceForm : LookupEntityBase
    {
        public int StepCount { get; set; }
        public List<EService> EServices { get; set; } = new List<EService>();

    }
}
