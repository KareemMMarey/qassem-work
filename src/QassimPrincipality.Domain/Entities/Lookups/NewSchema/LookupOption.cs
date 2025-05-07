using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Services.NewSchema;
using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class LookupOption : LookupEntityBase
	{
		public LookupOptionType LookupOptionType { get; set; }
		public virtual ICollection<RequestAdditionalData>? RequestAdditionalDataPrisonFrom { get; set; }
		public virtual ICollection<RequestAdditionalData>? RequestAdditionalDataPrisonTo { get; set; }
		public virtual ICollection<RequestAdditionalData>? RequestAdditionalOtherData { get; set; }

	}
}
