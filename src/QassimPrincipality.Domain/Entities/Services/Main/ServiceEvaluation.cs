using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.Main
{
    public class ServiceEvaluation : FullAuditedEntityBase<Guid>
    {
        public int SubCategoryId { get; set; }
        public int EvalutionValue { get; set; }
    }
}
