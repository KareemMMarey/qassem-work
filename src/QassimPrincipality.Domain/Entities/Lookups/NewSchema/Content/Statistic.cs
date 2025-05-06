using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content
{
    public class Statistic : LookupEntityBase
    {


        public string Value { get; set; } = string.Empty;

        public string IconClass { get; set; } = string.Empty; // يمكن تغييره إلى ImageUrl إذا كانت صورة

        public int OrderIndex { get; set; }
    }
}
